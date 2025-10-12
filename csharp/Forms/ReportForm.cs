using Microsoft.Data.Sqlite;
using System.Text;

namespace BookCatalog.Forms;

public partial class ReportForm : Form
{
    private string ConnectionString => $"Data Source={Program.GetDatabasePath()}";

    public ReportForm()
    {
        InitializeComponent();
        LoadReports();
    }

    private void LoadReports()
    {
        txtReport.Clear();
        
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var reportText = new StringBuilder();
            reportText.AppendLine("BOOK CATALOG STATISTICAL REPORT");
            reportText.AppendLine("Generated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            reportText.AppendLine("".PadRight(60, '='));
            reportText.AppendLine();

            // Total count
            var totalCommand = connection.CreateCommand();
            totalCommand.CommandText = "SELECT COUNT(*) FROM books";
            var totalBooks = Convert.ToInt32(totalCommand.ExecuteScalar());
            reportText.AppendLine($"📚 TOTAL BOOKS IN CATALOG: {totalBooks}");
            reportText.AppendLine();

            // Books by Genre
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Genre, COUNT(*) as Count 
                FROM books 
                GROUP BY Genre 
                ORDER BY Count DESC, Genre";

            reportText.AppendLine("📖 BOOKS BY GENRE:");
            reportText.AppendLine("".PadRight(40, '-'));
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var genre = reader.GetString(0);
                    var count = reader.GetInt32(1);
                    var percentage = totalBooks > 0 ? (double)count / totalBooks * 100 : 0;
                    reportText.AppendLine($"{genre.PadRight(20)} : {count,3} books ({percentage:F1}%)");
                }
            }
            reportText.AppendLine();

            // Books by Author
            command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Author, COUNT(*) as Count 
                FROM books 
                GROUP BY Author 
                ORDER BY Count DESC, Author
                LIMIT 15";

            reportText.AppendLine("👨‍💼 TOP 15 AUTHORS BY NUMBER OF BOOKS:");
            reportText.AppendLine("".PadRight(40, '-'));
            using (var reader = command.ExecuteReader())
            {
                int rank = 1;
                while (reader.Read())
                {
                    var author = reader.GetString(0);
                    var count = reader.GetInt32(1);
                    reportText.AppendLine($"{rank,2}. {author.PadRight(25)} : {count,3} books");
                    rank++;
                }
            }
            reportText.AppendLine();

            // Publication Years Analysis
            command = connection.CreateCommand();
            command.CommandText = @"
                SELECT 
                    CASE 
                        WHEN year >= 2020 THEN '2020s'
                        WHEN year >= 2010 THEN '2010s'
                        WHEN year >= 2000 THEN '2000s'
                        WHEN year >= 1990 THEN '1990s'
                        WHEN year >= 1980 THEN '1980s'
                        WHEN year >= 1970 THEN '1970s'
                        ELSE 'Before 1970'
                    END as Decade,
                    COUNT(*) as Count,
                    MIN(year) as MinYear,
                    MAX(year) as MaxYear
                FROM books 
                GROUP BY Decade 
                ORDER BY MIN(year) DESC";

            reportText.AppendLine("📅 BOOKS BY PUBLICATION DECADE:");
            reportText.AppendLine("".PadRight(40, '-'));
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var decade = reader.GetString(0);
                    var count = reader.GetInt32(1);
                    var minYear = reader.GetInt32(2);
                    var maxYear = reader.GetInt32(3);
                    var percentage = totalBooks > 0 ? (double)count / totalBooks * 100 : 0;
                    reportText.AppendLine($"{decade.PadRight(12)} : {count,3} books ({percentage:F1}%) [{minYear}-{maxYear}]");
                }
            }
            reportText.AppendLine();

            // Recent additions (last 10 books added - by ID)
            command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Title, Author, Genre, year 
                FROM books 
                ORDER BY Id DESC 
                LIMIT 10";

            reportText.AppendLine("🆕 RECENTLY ADDED BOOKS (Last 10):");
            reportText.AppendLine("".PadRight(40, '-'));
            using (var reader = command.ExecuteReader())
            {
                int count = 1;
                while (reader.Read())
                {
                    var title = reader.GetString(0);
                    var author = reader.GetString(1);
                    var genre = reader.GetString(2);
                    var year = reader.GetInt32(3);
                    
                    if (title.Length > 30) title = title.Substring(0, 27) + "...";
                    if (author.Length > 20) author = author.Substring(0, 17) + "...";
                    
                    reportText.AppendLine($"{count,2}. \"{title}\" by {author} ({year}) [{genre}]");
                    count++;
                }
            }

            reportText.AppendLine();
            reportText.AppendLine("".PadRight(60, '='));
            reportText.AppendLine("End of Report");

            txtReport.Text = reportText.ToString();
        }
        catch (Exception ex)
        {
            txtReport.Text = $"Error generating report: {ex.Message}";
        }
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        LoadReports();
        MessageBox.Show("Report refreshed successfully!", "Refresh Complete", 
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = "txt",
                FileName = $"BookCatalogReport_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, txtReport.Text);
                MessageBox.Show($"Report exported successfully to:\n{saveDialog.FileName}", 
                    "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}