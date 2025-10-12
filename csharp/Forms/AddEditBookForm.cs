using BookCatalog.Models;
using Microsoft.Data.Sqlite;

namespace BookCatalog.Forms;

public partial class AddEditBookForm : Form
{
    private string ConnectionString => $"Data Source={Program.GetDatabasePath()}";
    private readonly Book? _bookToEdit;

    public AddEditBookForm(Book? book = null)
    {
        InitializeComponent();
        _bookToEdit = book;

        if (book != null)
        {
            lblFormTitle.Text = "✏️ Edit Book";
            Text = "Edit Book";
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtGenre.Text = book.Genre;
            numYear.Value = book.PublicationYear;
        }
        else
        {
            lblFormTitle.Text = "📖 Add New Book";
            Text = "Add Book";
        }

        txtTitle.Focus();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
            return;

        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            if (_bookToEdit == null)
            {
                command.CommandText = @"INSERT INTO books (Title, Author, Genre, year) 
                                      VALUES (@title, @author, @genre, @year)";
            }
            else
            {
                command.CommandText = @"UPDATE books 
                                      SET Title = @title, Author = @author, 
                                          Genre = @genre, year = @year
                                      WHERE Id = @id";
                command.Parameters.AddWithValue("@id", _bookToEdit.Id);
            }

            command.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
            command.Parameters.AddWithValue("@author", txtAuthor.Text.Trim());
            command.Parameters.AddWithValue("@genre", txtGenre.Text.Trim());
            command.Parameters.AddWithValue("@year", (int)numYear.Value);

            command.ExecuteNonQuery();
            
            MessageBox.Show(
                _bookToEdit == null ? "Book added successfully!" : "Book updated successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving book: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool ValidateInput()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(txtTitle.Text))
            errors.Add("Title is required");
        
        if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            errors.Add("Author is required");
            
        if (string.IsNullOrWhiteSpace(txtGenre.Text))
            errors.Add("Genre is required");

        if (numYear.Value < 1000 || numYear.Value > DateTime.Now.Year + 10)
            errors.Add($"Publication year must be between 1000 and {DateTime.Now.Year + 10}");

        if (errors.Any())
        {
            MessageBox.Show(
                "Please fix the following errors:\n\n" + string.Join("\n", errors),
                "Validation Errors",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void txtTitle_Leave(object sender, EventArgs e)
    {
        txtTitle.Text = txtTitle.Text.Trim();
        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            txtTitle.BackColor = Color.LightPink;
        }
        else
        {
            txtTitle.BackColor = Color.White;
        }
    }

    private void txtAuthor_Leave(object sender, EventArgs e)
    {
        txtAuthor.Text = txtAuthor.Text.Trim();
        if (string.IsNullOrWhiteSpace(txtAuthor.Text))
        {
            txtAuthor.BackColor = Color.LightPink;
        }
        else
        {
            txtAuthor.BackColor = Color.White;
        }
    }

    private void txtGenre_Leave(object sender, EventArgs e)
    {
        txtGenre.Text = txtGenre.Text.Trim();
        if (string.IsNullOrWhiteSpace(txtGenre.Text))
        {
            txtGenre.BackColor = Color.LightPink;
        }
        else
        {
            txtGenre.BackColor = Color.White;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}