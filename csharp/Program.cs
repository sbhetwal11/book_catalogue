using Microsoft.Data.Sqlite;
using System;
using System.IO;

public class Program
{
    private static string dbPath = Path.Combine("..", "data", "books.db");

    public static void Main(string[] args)
    {
        InitializeDatabase();

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a command: add, list, search, or report.");
            return;
        }

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
        var dbDirectory = Path.GetDirectoryName(dbPath);
        if (!Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
        }

        using (var connection = new SqliteConnection($"Data Source={dbPath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS books (
                    id INTEGER PRIMARY KEY,
                    title TEXT NOT NULL,
                    author TEXT NOT NULL,
                    genre TEXT NOT NULL,
                    year INTEGER NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }
    }

    private static void AddBook()
    {
        Console.WriteLine("Enter book details:");
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Console.Write("Genre: ");
        string genre = Console.ReadLine();
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
            command.CommandText =
            @"
                INSERT INTO books (title, author, genre, year)
                VALUES (@title, @author, @genre, @year);
            ";
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@author", author);
            command.Parameters.AddWithValue("@genre", genre);
            command.Parameters.AddWithValue("@year", year);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Book added successfully! 👍");
    }

    private static void ListBooks()
    {
        Console.WriteLine("\n--- All Books ---");
        using (var connection = new SqliteConnection($"Data Source={dbPath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT title, author, genre, year FROM books ORDER BY title;";
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("No books found.");
                    return;
                }
                while (reader.Read())
                {
                    Console.WriteLine($"Title: {reader.GetString(0)}, Author: {reader.GetString(1)}, Genre: {reader.GetString(2)}, Year: {reader.GetInt32(3)}");
                }
            }
        }
    }

    private static void SearchBooks(string keyword)
    {
        Console.WriteLine($"\n--- Search Results for '{keyword}' ---");
        using (var connection = new SqliteConnection($"Data Source={dbPath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT title, author, genre, year
                FROM books
                WHERE title LIKE @keyword OR author LIKE @keyword OR genre LIKE @keyword
                ORDER BY title;
            ";
            command.Parameters.AddWithValue("@keyword", $"%{keyword}%");
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("No matching books found.");
                    return;
                }
                while (reader.Read())
                {
                    Console.WriteLine($"Title: {reader.GetString(0)}, Author: {reader.GetString(1)}, Genre: {reader.GetString(2)}, Year: {reader.GetInt32(3)}");
                }
            }
        }
    }

    private static void ReportBooks()
    {
        Console.WriteLine("\n--- Book Report ---");
        
        // Report by Genre
        Console.WriteLine("\n📊 By Genre:");
        using (var connection = new SqliteConnection($"Data Source={dbPath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT genre, COUNT(*) as count FROM books GROUP BY genre ORDER BY count DESC;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"- {reader.GetString(0)}: {reader.GetInt32(1)}");
                }
            }
        }

        // Report by Author
        Console.WriteLine("\n✍️ By Author:");
        using (var connection = new SqliteConnection($"Data Source={dbPath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT author, COUNT(*) as count FROM books GROUP BY author ORDER BY count DESC;";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"- {reader.GetString(0)}: {reader.GetInt32(1)}");
                }
            }
        }
    }
}