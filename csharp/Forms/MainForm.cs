using System.Data;
using BookCatalog.Models;
using Microsoft.Data.Sqlite;

namespace BookCatalog.Forms;

public partial class MainForm : Form
{
    private string ConnectionString => $"Data Source={Program.GetDatabasePath()}";
    private readonly DataTable _booksTable = new();

    public MainForm()
    {
        InitializeComponent();
        InitializeDataGrid();
        LoadBooks();
        UpdateButtonStates();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // Additional initialization if needed
        dgvBooks.Focus();
    }

    private void InitializeDataGrid()
    {
        _booksTable.Columns.Add("Id", typeof(int));
        _booksTable.Columns.Add("Title", typeof(string));
        _booksTable.Columns.Add("Author", typeof(string));
        _booksTable.Columns.Add("Genre", typeof(string));
        _booksTable.Columns.Add("Year", typeof(int));
        dgvBooks.DataSource = _booksTable;

        // Hide the ID column
        dgvBooks.Columns["Id"].Visible = false;
        
        // Set column headers and widths
        dgvBooks.Columns["Title"].HeaderText = "Book Title";
        dgvBooks.Columns["Title"].Width = 200;
        dgvBooks.Columns["Author"].HeaderText = "Author";
        dgvBooks.Columns["Author"].Width = 150;
        dgvBooks.Columns["Genre"].HeaderText = "Genre";
        dgvBooks.Columns["Genre"].Width = 120;
        dgvBooks.Columns["Year"].HeaderText = "Publication Year";
        dgvBooks.Columns["Year"].Width = 120;

        // Set alternating row colors
        dgvBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        dgvBooks.RowsDefaultCellStyle.BackColor = Color.White;
        dgvBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
        dgvBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
    }

    private void LoadBooks(string searchTerm = "")
    {
        _booksTable.Clear();
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            command.CommandText = "SELECT * FROM books ORDER BY Title";
        }
        else
        {
            command.CommandText = @"SELECT * FROM books 
                WHERE Title LIKE @search 
                OR Author LIKE @search 
                OR Genre LIKE @search 
                ORDER BY Title";
            command.Parameters.AddWithValue("@search", $"%{searchTerm}%");
        }

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            _booksTable.Rows.Add(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetInt32(4)
            );
        }

        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        bool hasSelectedRow = dgvBooks.CurrentRow != null;
        btnEdit.Enabled = hasSelectedRow;
        btnDelete.Enabled = hasSelectedRow;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        LoadBooks(txtSearch.Text);
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var addForm = new AddEditBookForm();
        if (addForm.ShowDialog() == DialogResult.OK)
        {
            LoadBooks();
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (dgvBooks.CurrentRow == null) return;
        
        var book = new Book
        {
            Id = (int)dgvBooks.CurrentRow.Cells["Id"].Value,
            Title = (string)dgvBooks.CurrentRow.Cells["Title"].Value,
            Author = (string)dgvBooks.CurrentRow.Cells["Author"].Value,
            Genre = (string)dgvBooks.CurrentRow.Cells["Genre"].Value,
            PublicationYear = (int)dgvBooks.CurrentRow.Cells["Year"].Value
        };

        var editForm = new AddEditBookForm(book);
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            LoadBooks();
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvBooks.CurrentRow == null) return;

        var id = (int)dgvBooks.CurrentRow.Cells["Id"].Value;
        var title = (string)dgvBooks.CurrentRow.Cells["Title"].Value;

        if (MessageBox.Show($"Are you sure you want to delete '{title}'?", 
            "Confirm Delete", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM books WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();

        LoadBooks();
    }

    private void btnReport_Click(object sender, EventArgs e)
    {
        var reportForm = new ReportForm();
        reportForm.Show();
    }

    private void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadBooks();
        txtSearch.Focus();
    }

    private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            btnSearch_Click(sender, e);
            e.Handled = true;
        }
    }

    private void dgvBooks_SelectionChanged(object sender, EventArgs e)
    {
        UpdateButtonStates();
    }

    private void dgvBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        // Format publication year to be more readable
        if (dgvBooks.Columns[e.ColumnIndex].Name == "Year" && e.Value != null)
        {
            e.Value = e.Value.ToString();
            e.FormattingApplied = true;
        }
    }
}