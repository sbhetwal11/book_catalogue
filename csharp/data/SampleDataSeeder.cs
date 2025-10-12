using Microsoft.Data.Sqlite;

namespace BookCatalog.Data;

public static class SampleDataSeeder
{
    public static void SeedSampleData(string connectionString)
    {
        try
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            // Check if data already exists
            var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = "SELECT COUNT(*) FROM books";
            var existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
            
            if (existingCount > 0)
            {
                return; // Data already exists, don't seed
            }

            var sampleBooks = new[]
            {
                new { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic Literature", Year = 1925 },
                new { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic Literature", Year = 1960 },
                new { Title = "1984", Author = "George Orwell", Genre = "Dystopian Fiction", Year = 1949 },
                new { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Year = 1813 },
                new { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Genre = "Coming-of-age Fiction", Year = 1951 },
                new { Title = "Lord of the Flies", Author = "William Golding", Genre = "Adventure Fiction", Year = 1954 },
                new { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = 1954 },
                new { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Genre = "Fantasy", Year = 1997 },
                new { Title = "The Chronicles of Narnia", Author = "C.S. Lewis", Genre = "Fantasy", Year = 1950 },
                new { Title = "Dune", Author = "Frank Herbert", Genre = "Science Fiction", Year = 1965 },
                new { Title = "Foundation", Author = "Isaac Asimov", Genre = "Science Fiction", Year = 1951 },
                new { Title = "Brave New World", Author = "Aldous Huxley", Genre = "Dystopian Fiction", Year = 1932 },
                new { Title = "The Handmaid's Tale", Author = "Margaret Atwood", Genre = "Dystopian Fiction", Year = 1985 },
                new { Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", Genre = "Psychological Fiction", Year = 1866 },
                new { Title = "War and Peace", Author = "Leo Tolstoy", Genre = "Historical Fiction", Year = 1869 },
                new { Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = 1937 },
                new { Title = "Jane Eyre", Author = "Charlotte Brontë", Genre = "Gothic Fiction", Year = 1847 },
                new { Title = "Wuthering Heights", Author = "Emily Brontë", Genre = "Gothic Fiction", Year = 1847 },
                new { Title = "The Picture of Dorian Gray", Author = "Oscar Wilde", Genre = "Gothic Fiction", Year = 1890 },
                new { Title = "Frankenstein", Author = "Mary Shelley", Genre = "Gothic Fiction", Year = 1818 },
                new { Title = "Dracula", Author = "Bram Stoker", Genre = "Gothic Fiction", Year = 1897 },
                new { Title = "The Adventures of Sherlock Holmes", Author = "Arthur Conan Doyle", Genre = "Mystery", Year = 1892 },
                new { Title = "Agatha Christie's Poirot", Author = "Agatha Christie", Genre = "Mystery", Year = 1920 },
                new { Title = "The Da Vinci Code", Author = "Dan Brown", Genre = "Thriller", Year = 2003 },
                new { Title = "Gone Girl", Author = "Gillian Flynn", Genre = "Psychological Thriller", Year = 2012 }
            };

            foreach (var book in sampleBooks)
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO books (Title, Author, Genre, year)
                    VALUES (@title, @author, @genre, @year)";
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@genre", book.Genre);
                command.Parameters.AddWithValue("@year", book.Year);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Silently fail - this is just sample data
            System.Diagnostics.Debug.WriteLine($"Error seeding sample data: {ex.Message}");
        }
    }
}