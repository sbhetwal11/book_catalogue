using Microsoft.Data.Sqlite;
using BookCatalog.Forms;
using BookCatalog.Data;

namespace BookCatalog
{
    public class Program
    {
        public static string GetDatabasePath()
        {
            var baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var projectDir = Path.GetFullPath(Path.Combine(baseDir!, "..", "..", ".."));
            var dataDir = Path.Combine(projectDir, "data");
            Directory.CreateDirectory(dataDir);
            return Path.Combine(dataDir, "books.db");
        }

        [STAThread]
        public static void Main(string[] args)
        {
            InitializeDatabase();
            
            // Seed sample data if database is empty
            SampleDataSeeder.SeedSampleData($"Data Source={GetDatabasePath()}");

            // If command line arguments provided, run console mode
            if (args.Length > 0)
            {
                RunConsoleMode(args);
                return;
            }

            // Otherwise run Windows Forms GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void RunConsoleMode(string[] args)
        {
            string command = args[0].ToLower();

            switch (command)
            {
                case "add":
                    AddBook();
                    break;
                case "list":
                    ListBooks();
                    break;
                case "search":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Please provide a search keyword.");
                        return;
                    }
                    SearchBooks(args[1]);
                    break;
                case "report":
                    ReportBooks();
                    break;
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }

        private static void InitializeDatabase()
        {
            var dbPath = GetDatabasePath();
            var dbDirectory = Path.GetDirectoryName(dbPath)!;
            if (!Directory.Exists(dbDirectory))
            {
                Directory.CreateDirectory(dbDirectory);
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS books (
                        id INTEGER PRIMARY KEY,
                        title TEXT NOT NULL,
                        author TEXT NOT NULL,
                        genre TEXT NOT NULL,
                        year INTEGER NOT NULL
                    );";
                command.ExecuteNonQuery();
            }
        }

        private static void AddBook()
        {
            var dbPath = GetDatabasePath();
            Console.WriteLine("Enter book details:");
            Console.Write("Title: ");
            string title = Console.ReadLine()!;
            Console.Write("Author: ");
            string author = Console.ReadLine()!;
            Console.Write("Genre: ");
            string genre = Console.ReadLine()!;
            Console.Write("Year: ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year))
            {
                Console.WriteLine("Invalid year. Please enter a number.");
                Console.Write("Year: ");
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO books (title, author, genre, year)
                    VALUES (@title, @author, @genre, @year);";
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@genre", genre);
                command.Parameters.AddWithValue("@year", year);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Book added successfully!");
        }

        private static void ListBooks()
        {
            Console.WriteLine("\n--- All Books ---");
            var allBooks = GetAllBooks();

            if (!allBooks.Any())
            {
                Console.WriteLine("No books found.");
                return;
            }

            var sortedBooks = allBooks.OrderBy(b => b.Title);
            foreach (var book in sortedBooks)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Year: {book.PublicationYear}");
            }
        }

        private static void SearchBooks(string keyword)
        {
            Console.WriteLine($"\n--- Search Results for '{keyword}' ---");
            var allBooks = GetAllBooks();
            var matchingBooks = allBooks
                .Where(b => b.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            b.Author.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            b.Genre.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(b => b.Title);

            if (!matchingBooks.Any())
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            foreach (var book in matchingBooks)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Year: {book.PublicationYear}");
            }
        }

        private static void ReportBooks()
        {
            Console.WriteLine("\n--- Book Report ---");
            var allBooks = GetAllBooks();

            Console.WriteLine("\nBy Genre:");
            var genreCounts = allBooks
                .GroupBy(b => b.Genre)
                .Select(g => new { Genre = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count);

            foreach (var report in genreCounts)
            {
                Console.WriteLine($"- {report.Genre}: {report.Count}");
            }

            Console.WriteLine("\nBy Author:");
            var authorCounts = allBooks
                .GroupBy(b => b.Author)
                .Select(g => new { Author = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count);

            foreach (var report in authorCounts)
            {
                Console.WriteLine($"- {report.Author}: {report.Count}");
            }
        }

        private static List<BookCatalog.Models.Book> GetAllBooks()
        {
            var dbPath = GetDatabasePath();
            var books = new List<BookCatalog.Models.Book>();
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT id, title, author, genre, year FROM books;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new BookCatalog.Models.Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Genre = reader.GetString(3),
                            PublicationYear = reader.GetInt32(4)
                        });
                    }
                }
            }
            return books;
        }
    }
}