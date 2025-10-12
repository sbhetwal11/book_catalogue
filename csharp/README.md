# Book Catalog Management System (C#)

A comprehensive GUI-based book cataloging application built with **C# .NET 8** and **Windows Forms**, featuring SQLite database integration for efficient book management.

## Project Overview

This application is part of a cross-language development project that demonstrates how different programming languages handle similar functionalities. The C# implementation showcases object-oriented programming, Windows Forms GUI development, and database integration patterns specific to the .NET ecosystem.

## Features

### Core Functionality
-  **Add Books**: Create new book entries with validation
-  **Edit Books**: Modify existing book information
-  **Delete Books**: Remove books with confirmation dialogs
-  **Search Books**: Find books by title, author, or genre
-  **View All Books**: Browse complete catalog in a data grid
-  **Generate Reports**: Comprehensive statistics and analytics

### Advanced Features
- **Modern GUI**: Professional Windows Forms interface with icons and colors
- **Real-time Search**: Instant filtering with Enter key support
- **Rich Reports**: Statistics by genre, author, publication decade
- **Export Reports**: Save reports to text files
- **Input Validation**: Real-time field validation with visual feedback
- **Sample Data**: 25 pre-loaded books for testing
- **Dual Mode**: Both GUI and console interfaces

##  Technology Stack

- **Language**: C# 12 (.NET 8)
- **GUI Framework**: Windows Forms
- **Database**: SQLite with Microsoft.Data.Sqlite
- **Architecture**: Object-Oriented Programming (OOP)
- **Design Patterns**: Model-View-Controller (MVC), Repository Pattern

##  Quick Start

### Prerequisites
- **.NET 8 SDK** or later
- **Windows OS** (for Windows Forms)
- **Visual Studio 2022** or **VS Code** (recommended)

### Installation & Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Svadlapat/book_catalogue.git
   cd book_catalogue/csharp
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the application**:
   ```bash
   dotnet build
   ```

4. **Run the GUI application**:
   ```bash
   dotnet run
   ```
   *Or navigate to `bin/Debug/net8.0-windows/` and run `csharp.exe`*

### Console Mode Usage
```bash
# Add a book via console
dotnet run -- add

# List all books
dotnet run -- list

# Search books
dotnet run -- search "fantasy"

# Generate report
dotnet run -- report
```

##  Usage Guide

### Main Interface

- **Search Box**: Type to filter books by title, author, or genre
- **Add Button**: Opens form to add new books
- **Edit Button**: Modify selected book (enabled when row selected)
- **Delete Button**: Remove selected book with confirmation
- **Generate Report**: Opens detailed statistics window

### Adding/Editing Books
- **Title**: Book title (required)
- **Author**: Author name (required)
- **Genre**: Book genre/category (required)
- **Publication Year**: Year published (1000-2030)

### Reports & Analytics
The report feature provides:
-  Total book count
-  Books by genre with percentages
-  Top 15 authors by book count
-  Books by publication decade
-  Recently added books

##  Project Structure

```
csharp/
├── Data/
│   └── SampleDataSeeder.cs      # Sample data generation
├── Forms/
│   ├── MainForm.cs/.Designer.cs # Main application window
│   ├── AddEditBookForm.cs/.Designer.cs # Add/Edit book dialog
│   └── ReportForm.cs/.Designer.cs # Statistics report window
├── Models/
│   └── Book.cs                  # Book data model
├── Program.cs                   # Application entry point
├── csharp.csproj               # Project configuration
└── data/
    └── books.db                # SQLite database (auto-created)
```

##  C# Language-Specific Features Demonstrated

### Object-Oriented Programming
```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
}
```

### LINQ Queries
```csharp
var sortedBooks = allBooks.OrderBy(b => b.Title);
var genreCounts = allBooks
    .GroupBy(b => b.Genre)
    .Select(g => new { Genre = g.Key, Count = g.Count() })
    .OrderByDescending(g => g.Count);
```

### Windows Forms Event Handling
```csharp
private void btnSearch_Click(object sender, EventArgs e)
{
    LoadBooks(txtSearch.Text);
}
```

### Exception Handling
```csharp
try
{
    // Database operations
}
catch (Exception ex)
{
    MessageBox.Show($"Error: {ex.Message}", "Error", 
        MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

### String Interpolation & StringBuilder
```csharp
var connectionString = $"Data Source={GetDatabasePath()}";
var report = new StringBuilder();
report.AppendLine($"Total Books: {totalCount}");
```

##  Database Schema

```sql
CREATE TABLE books (
    id INTEGER PRIMARY KEY,
    title TEXT NOT NULL,
    author TEXT NOT NULL,
    genre TEXT NOT NULL,
    year INTEGER NOT NULL
);
```

##  Sample Data

The application includes 25 pre-loaded books covering various genres:
- Classic Literature (The Great Gatsby, Pride and Prejudice)
- Fantasy (Lord of the Rings, Harry Potter)
- Science Fiction (Dune, Foundation)
- Mystery (Sherlock Holmes, Agatha Christie)
- And more...

##  Requirements Compliance

### Project Requirements 
- **Storage for book details**:  Title, Author, Genre, Publication Year
- **Search functionality**:  Multi-field search capability
- **Simple reporting**:  Statistics by genre and author
- **GUI-based application**:  Windows Forms interface

### C# Language Features 
- **Classes and Properties**:  Book model with auto-properties
- **LINQ for Querying**:  Data filtering and grouping
- **Collections Handling**:  List<T>, DataTable usage
- **Exception Handling**:  Try-catch with user feedback
- **Memory Management**:  Automatic garbage collection
- **Event-Driven Programming**:  Windows Forms events

##  Build & Deployment

### Development Build
```bash
dotnet build --configuration Debug
```

### Release Build
```bash
dotnet build --configuration Release
```

### Publishing
```bash
dotnet publish --configuration Release --runtime win-x64 --self-contained true
```

##  Testing

### Manual Testing
1. Launch the application
2. Test CRUD operations (Create, Read, Update, Delete)
3. Verify search functionality
4. Generate and export reports
5. Test input validation

### Console Mode Testing
```bash
# Test console commands
dotnet run -- add
dotnet run -- list
dotnet run -- search "tolkien"
dotnet run -- report
```

##  Configuration

### Database Path
The SQLite database is automatically created in:
```
{ProjectRoot}/data/books.db
```

### Customization
- Modify `SampleDataSeeder.cs` to change initial data
- Update `Book.cs` model for additional fields
- Customize UI in `.Designer.cs` files

##  Cross-Language Comparison

This C# implementation provides excellent contrast points for comparison with other language implementations:

### C# Strengths
- **Strong Typing**: Compile-time error checking
- **Rich GUI Framework**: Windows Forms with visual designer
- **Object-Oriented**: Natural class-based modeling
- **LINQ Integration**: Powerful querying capabilities
- **Memory Management**: Automatic garbage collection
- **Tooling**: Excellent IDE support and debugging

### Comparison Areas
- **Performance**: Compiled vs interpreted languages
- **Syntax**: Verbose vs concise language constructs
- **Type System**: Static vs dynamic typing
- **Memory**: Managed vs manual memory management
- **Platform**: Windows-focused vs cross-platform

