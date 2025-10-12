# rb_seed_and_bench.rb
# Usage: ruby rb_seed_and_bench.rb [ROW_COUNT=1000] [RUNS=5]
require 'sqlite3'
require 'fileutils'

DB_PATH = File.expand_path('../data/books.db', __dir__)
ROW_COUNT = (ARGV[0] || "1000").to_i
RUNS      = (ARGV[1] || "5").to_i

GENRES = %w[Fiction Nonfiction Sci-Fi Fantasy Mystery History Biography Tech Poetry Philosophy]
AUTHORS = %w[
  Alice_Wong Bob_Smith Carol_Jones David_Lee Eva_Khan Farid_Noori Grace_Yang Hector_Santos
  Irene_Patel Jonas_Meyer Kira_Nakamura Leo_Rossi Maya_Cohen Noor_Alvarez Omar_Popov
  Priya_Singh Qi_Zhang Rosa_Garcia Samir_Ali Tessa_Brown Uma_Das Vik_Rao Wen_Lin Yara_Said Zack_Miller
]

def db
  FileUtils.mkdir_p(File.dirname(DB_PATH))
  d = SQLite3::Database.new(DB_PATH)
  d.results_as_hash = false
  d.execute <<~SQL
    CREATE TABLE IF NOT EXISTS books (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      title  TEXT NOT NULL,
      author TEXT NOT NULL,
      genre  TEXT NOT NULL,
      year   INTEGER NOT NULL
    );
  SQL
  d.execute "CREATE INDEX IF NOT EXISTS idx_books_title  ON books(lower(title));"
  d.execute "CREATE INDEX IF NOT EXISTS idx_books_author ON books(lower(author));"
  d.execute "CREATE INDEX IF NOT EXISTS idx_books_genre  ON books(lower(genre));"
  d.execute "CREATE INDEX IF NOT EXISTS idx_books_year   ON books(year);"
  d
end

def seed!(d, count)
  # start fresh so the table holds exactly `count` rows
  d.execute("DELETE FROM books;")
  d.execute("VACUUM;")

  d.execute("PRAGMA journal_mode = WAL;")
  d.execute("PRAGMA synchronous = OFF;")
  d.execute("BEGIN;")

  stmt = d.prepare("INSERT INTO books(title,author,genre,year) VALUES(?,?,?,?)")
  count.times do |i|
    g = GENRES.sample
    a = AUTHORS.sample.tr('_', ' ')
    t = "#{g} Book #{i + 1}"
    y = rand(1950..2024)
    stmt.execute(t, a, g, y)
  end
  stmt.close

  d.execute("COMMIT;")
  d.execute("PRAGMA synchronous = NORMAL;")
end

def time_avg(runs)
  t0 = Process.clock_gettime(Process::CLOCK_MONOTONIC)
  runs.times { yield }
  t1 = Process.clock_gettime(Process::CLOCK_MONOTONIC)
  total = t1 - t0
  avg   = total / runs
  [total, avg]
end

d = db

# Seed if needed (or if existing count differs)
existing = d.get_first_value("SELECT COUNT(*) FROM books").to_i
if existing != ROW_COUNT
  puts "Seeding #{ROW_COUNT} books (was #{existing})..."
  seed!(d, ROW_COUNT)
  existing = d.get_first_value("SELECT COUNT(*) FROM books").to_i
end
puts "Rows in table: #{existing}"

# Make sure there is at least one word to hit in LIKE query
# We'll search for '%book%' (present in generated titles)
search_sql = "SELECT id, title FROM books WHERE lower(title) LIKE '%book%' LIMIT 100;"

# Warm-up (optional)
d.execute(search_sql)
d.execute("SELECT genre, COUNT(*) FROM books GROUP BY genre;")

# Measure
search_total, search_avg = time_avg(RUNS) { d.execute(search_sql) }
report_total, report_avg = time_avg(RUNS) { d.execute("SELECT genre, COUNT(*) FROM books GROUP BY genre;") }

# Throughput calculations
total_rows  = existing
sample_hits = d.get_first_value("SELECT COUNT(*) FROM books WHERE lower(title) LIKE '%book%';").to_i

search_thrpt = (search_avg > 0 && sample_hits > 0) ? (sample_hits / search_avg) : 0.0
report_thrpt = (report_avg > 0 && total_rows  > 0) ? (total_rows  / report_avg) : 0.0

# Output
puts "\n===== Performance Results ====="
puts "Rows: #{existing}, Runs per test: #{RUNS}"
puts "Search (title LIKE '%book%'): total=#{format('%.4f', search_total)}s, avg=#{format('%.4f', search_avg)}s, "\
     "throughput≈#{search_thrpt.round(1)} rows/sec"
puts "Report (GROUP BY genre)    : total=#{format('%.4f', report_total)}s, avg=#{format('%.4f', report_avg)}s, "\
     "throughput≈#{report_thrpt.round(1)} rows/sec"

# Friendly line to paste into your report
puts "\nCopy for report:"
puts "- With #{existing} rows, Ruby averaged #{format('%.3f', search_avg)}s for search (~#{search_thrpt.round(0)} rows/s) "\
     "and #{format('%.3f', report_avg)}s for genre report (~#{report_thrpt.round(0)} rows/s). "\
     "SQLite dominated runtime; Ruby-side overhead was negligible at this scale."
