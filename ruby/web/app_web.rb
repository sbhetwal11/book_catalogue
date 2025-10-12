# app_web.rb
require 'sinatra'
require 'sqlite3'
require 'csv'
require 'json'
require 'fileutils'
require 'rack/utils'
require 'webrick'
set :server, :webrick

#SAME DB as  CLI & C# apps
DB_PATH = File.expand_path('../../data/books.db', __dir__)
FileUtils.mkdir_p(File.dirname(DB_PATH))

# --- DB utilities ---
def with_db
  db = SQLite3::Database.new(DB_PATH)
  db.results_as_hash = true
  yield db
ensure
  db&.close
end

def init_db
  with_db do |db|
    db.execute <<~SQL
      CREATE TABLE IF NOT EXISTS books (
        id     INTEGER PRIMARY KEY,
        title  TEXT NOT NULL,
        author TEXT NOT NULL,
        genre  TEXT NOT NULL,
        year   INTEGER NOT NULL
      );
    SQL
  end
end

before { init_db }

helpers do
  def h(s) Rack::Utils.escape_html(s.to_s) end
end

# --- Routes ---

# Home: list (optionally filtered by ?kw=)
get '/' do
  kw = params['kw'].to_s.strip
  @books = with_db do |db|
    if kw.empty?
      db.execute("SELECT * FROM books ORDER BY title;")
    else
      like = "%#{kw}%"
      db.execute("SELECT * FROM books
                  WHERE title LIKE ? OR author LIKE ? OR genre LIKE ?
                  ORDER BY title;", [like, like, like])
    end
  end
  @kw = kw
  erb :index
end

# Add form
get '/new' do
  erb :new
end

# Create book (with duplicate guard)
post '/create' do
  title  = params['title'].to_s.strip
  author = params['author'].to_s.strip
  genre  = params['genre'].to_s.strip
  year_s = params['year'].to_s.strip

  unless !title.empty? && !author.empty? && !genre.empty? && year_s =~ /^\d{1,4}$/
    @error = "All fields required. Year must be a number."
    return erb :new
  end

  year = year_s.to_i

  dup = with_db do |db|
    db.get_first_value("SELECT COUNT(*) FROM books
                        WHERE lower(title)=? AND lower(author)=? AND year=?;",
                        [title.downcase, author.downcase, year])
  end

  if dup.to_i > 0
    @error = "Duplicate (same Title+Author+Year). Not added."
    return erb :new
  end

  with_db do |db|
    db.execute("INSERT INTO books (title, author, genre, year)
                VALUES (?,?,?,?);", [title, author, genre, year])
  end

  redirect '/'
end

# Delete (small convenience for demo)
post '/delete/:id' do
  with_db { |db| db.execute("DELETE FROM books WHERE id=?;", [params['id'].to_i]) }
  redirect '/'
end

# Report (group=genre|author)
get '/report' do
  @group = %w[genre author].include?(params['group']) ? params['group'] : 'genre'
  @rows = with_db do |db|
    db.execute("SELECT #{@group} AS label, COUNT(*) AS count
                FROM books
                GROUP BY #{@group}
                ORDER BY count DESC;")
  end
  erb :report
end

# Export CSV
get '/export.csv' do
  content_type 'text/csv'
  attachment 'books.csv'
  rows = with_db { |db| db.execute("SELECT title, author, genre, year FROM books ORDER BY title;") }
  CSV.generate do |csv|
    csv << %w[Title Author Genre Year]
    rows.each { |r| csv << [r['title'], r['author'], r['genre'], r['year']] }
  end
end

# Export JSON
get '/export.json' do
  content_type 'application/json'
  attachment 'books.json'
  rows = with_db { |db| db.execute("SELECT title, author, genre, year FROM books ORDER BY title;") }
  JSON.pretty_generate(rows.map { |r|
    { title: r['title'], author: r['author'], genre: r['genre'], year: r['year'] }
  })
end
