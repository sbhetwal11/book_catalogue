namespace BookCatalog.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvBooks = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnReport = new Button();
            lblSearch = new Label();
            pnlTop = new Panel();
            pnlBottom = new Panel();
            btnClearSearch = new Button();
            lblTitle = new Label();
            
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 35);
            lblTitle.TabIndex = 7;
            lblTitle.Text = "📚 Book Catalog System";
            
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.LightGray;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(btnClearSearch);
            pnlTop.Controls.Add(btnDelete);
            pnlTop.Controls.Add(btnEdit);
            pnlTop.Controls.Add(btnAdd);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Controls.Add(lblSearch);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(884, 80);
            pnlTop.TabIndex = 8;
            
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.Location = new Point(12, 52);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(49, 15);
            lblSearch.TabIndex = 8;
            lblSearch.Text = "Search:";
            
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(67, 49);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by title, author, or genre...";
            txtSearch.Size = new Size(250, 23);
            txtSearch.TabIndex = 1;
            txtSearch.KeyPress += txtSearch_KeyPress;
            
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightBlue;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(323, 49);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "🔍 Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            
            // 
            // btnClearSearch
            // 
            btnClearSearch.BackColor = Color.LightGray;
            btnClearSearch.FlatStyle = FlatStyle.Flat;
            btnClearSearch.Font = new Font("Segoe UI", 9F);
            btnClearSearch.Location = new Point(404, 49);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(50, 23);
            btnClearSearch.TabIndex = 9;
            btnClearSearch.Text = "Clear";
            btnClearSearch.UseVisualStyleBackColor = false;
            btnClearSearch.Click += btnClearSearch_Click;
            
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(634, 49);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "➕ Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEdit.BackColor = Color.LightYellow;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEdit.Location = new Point(715, 49);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 23);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "✏️ Edit";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(796, 49);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "🗑️ Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.BackgroundColor = Color.White;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Dock = DockStyle.Fill;
            dgvBooks.Location = new Point(0, 80);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.RowTemplate.Height = 25;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(884, 390);
            dgvBooks.TabIndex = 0;
            dgvBooks.CellFormatting += dgvBooks_CellFormatting;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
            
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.LightGray;
            pnlBottom.Controls.Add(btnReport);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 470);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(884, 50);
            pnlBottom.TabIndex = 9;
            
            // 
            // btnReport
            // 
            btnReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnReport.BackColor = Color.LightSteelBlue;
            btnReport.FlatStyle = FlatStyle.Flat;
            btnReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReport.Location = new Point(750, 14);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(120, 23);
            btnReport.TabIndex = 6;
            btnReport.Text = "📊 Generate Report";
            btnReport.UseVisualStyleBackColor = false;
            btnReport.Click += btnReport_Click;
            
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(884, 520);
            Controls.Add(dgvBooks);
            Controls.Add(pnlTop);
            Controls.Add(pnlBottom);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(800, 500);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Book Catalog Management System";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private DataGridView dgvBooks;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnReport;
        private Label lblSearch;
        private Panel pnlTop;
        private Panel pnlBottom;
        private Button btnClearSearch;
        private Label lblTitle;
    }
}