# app_sqlite.rb — CLI: add | list | search | report | export | quit
require 'sqlite3'
require 'fileutils'
require 'csv'      # for CSV export
require 'json'     # for JSON export

DB_PATH = File.expand_path('../data/books.db', __dir__)

def db
  FileUtils.mkdir_p(File.dirname(DB_PATH))
  @db ||= begin
    d = SQLite3::Database.new(DB_PATH)
    d.results_as_hash = true
    d.execute <<~SQL
      CREATE TABLE IF NOT EXISTS books (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        title  TEXT NOT NULL,
        author TEXT NOT NULL,
        genre  TEXT NOT NULL,
        year   INTEGER NOT NULL
      );
    SQL
    # (Optional) helpful indexes for faster search/report
    d.execute "CREATE INDEX IF NOT EXISTS idx_books_title  ON books(lower(title));"
    d.execute "CREATE INDEX IF NOT EXISTS idx_books_author ON books(lower(author));"
    d.execute "CREATE INDEX IF NOT EXISTS idx_books_genre  ON books(lower(genre));"
    d.execute "CREATE INDEX IF NOT EXISTS idx_books_year   ON books(year);"
    d
  end
end

def add_book
  print "Title: "  ; title  = STDIN.gets&.strip.to_s
  print "Author: " ; author = STDIN.gets&.strip.to_s
  print "Genre: "  ; genre  = STDIN.gets&.strip.to_s
  print "Year: "   ; year_s = STDIN.gets&.strip.to_s
  year = Integer(year_s) rescue nil

  if title.empty? || author.empty? || genre.empty? || year.nil?
    puts "Please enter valid Title/Author/Genre and a numeric Year."
    return
  end

  # Duplicate guard: same title+author+year (case-insensitive for title/author)
  exists = db.get_first_value(
    "SELECT COUNT(*) FROM books WHERE lower(title)=? AND lower(author)=? AND year=?",
    [title.downcase, author.downcase, year]
  ).to_i

  if exists > 0
    puts "Duplicate detected (same title+author+year). Not added."
    return
  end

  db.execute("INSERT INTO books(title,author,genre,year) VALUES(?,?,?,?)",
             [title, author, genre, year])
  puts "Added."
end

def list_books
  rows = db.execute("SELECT id,title,author,genre,year FROM books ORDER BY id")
  if rows.empty?
    puts "(no books yet)"
  else
    rows.each { |r| puts "##{r['id']} #{r['title']} — #{r['author']} [#{r['genre']}] (#{r['year']})" }
  end
end

def search_books
  print "Keyword (title/author/genre): "
  kw = (STDIN.gets&.strip || "").downcase
  if kw.empty?
    puts "Enter a keyword."
    return
  end
  rows = db.execute(<<~SQL, ["%#{kw}%", "%#{kw}%", "%#{kw}%"])
    SELECT id,title,author,genre,year
    FROM books
    WHERE lower(title)  LIKE ?
       OR lower(author) LIKE ?
       OR lower(genre)  LIKE ?
    ORDER BY id
  SQL
  if rows.empty?
    puts "(no matches)"
  else
    rows.each { |r| puts "##{r['id']} #{r['title']} — #{r['author']} [#{r['genre']}] (#{r['year']})" }
  end
end

def report
  print "Group by (genre/author): "
  choice = (STDIN.gets&.strip || "").downcase
  col = case choice
        when "genre"  then "genre"
        when "author" then "author"
        else
          puts "Choose 'genre' or 'author'."; return
        end
  rows = db.execute("SELECT #{col} AS key, COUNT(*) AS cnt FROM books GROUP BY #{col} ORDER BY cnt DESC, key ASC")
  if rows.empty?
    puts "(no data yet)"
  else
    rows.each { |r| puts "#{r['key']}: #{r['cnt']}" }
  end
end

def export_csv
  rows = db.execute("SELECT id,title,author,genre,year FROM books ORDER BY id")
  path = File.expand_path('../data/books.csv', __dir__)
  CSV.open(path, 'w', write_headers: true, headers: %w[id title author genre year]) do |csv|
    rows.each { |r| csv << [r['id'], r['title'], r['author'], r['genre'], r['year']] }
  end
  puts "Exported #{rows.size} book(s) to #{path}"
end

def export_json
  rows = db.execute("SELECT id,title,author,genre,year FROM books ORDER BY id")
  payload = rows.map { |r| { id: r['id'], title: r['title'], author: r['author'], genre: r['genre'], year: r['year'] } }
  path = File.expand_path('../data/books.json', __dir__)
  File.write(path, JSON.pretty_generate(payload))
  puts "Exported #{rows.size} book(s) to #{path}"
end

def export_data
  print "Format (csv/json): "
  fmt = (STDIN.gets&.strip || "").downcase
  case fmt
  when "csv"  then export_csv
  when "json" then export_json
  else puts "Unknown format. Try 'csv' or 'json'."
  end
end

puts "Book Catalog CLI Ready. Commands: add | list | search | report | export | quit"
while (print("> "); line = STDIN.gets)
  case line.strip.downcase
  when "add"    then add_book
  when "list"   then list_books
  when "search" then search_books
  when "report" then report
  when "export" then export_data
  when "quit", "exit" then break
  when ""      then next
  else puts "Unknown command. Try: add | list | search | report | export | quit"
  end
end
