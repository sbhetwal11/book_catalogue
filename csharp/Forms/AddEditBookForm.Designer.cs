namespace BookCatalog.Forms
{
    partial class AddEditBookForm
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
            lblTitle = new Label();
            txtTitle = new TextBox();
            lblAuthor = new Label();
            txtAuthor = new TextBox();
            lblGenre = new Label();
            txtGenre = new TextBox();
            lblYear = new Label();
            numYear = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();
            pnlMain = new Panel();
            lblFormTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)numYear).BeginInit();
            pnlMain.SuspendLayout();
            SuspendLayout();
            
            // 
            // lblFormTitle
            // 
            lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.DarkBlue;
            lblFormTitle.Location = new Point(12, 12);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(360, 25);
            lblFormTitle.TabIndex = 10;
            lblFormTitle.Text = "📖 Add New Book";
            lblFormTitle.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.WhiteSmoke;
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Controls.Add(txtTitle);
            pnlMain.Controls.Add(lblAuthor);
            pnlMain.Controls.Add(txtAuthor);
            pnlMain.Controls.Add(lblGenre);
            pnlMain.Controls.Add(txtGenre);
            pnlMain.Controls.Add(lblYear);
            pnlMain.Controls.Add(numYear);
            pnlMain.Location = new Point(12, 50);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(360, 180);
            pnlMain.TabIndex = 11;
            
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(67, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Book Title:";
            
            // 
            // txtTitle
            // 
            txtTitle.Font = new Font("Segoe UI", 9F);
            txtTitle.Location = new Point(115, 17);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(230, 23);
            txtTitle.TabIndex = 1;
            txtTitle.Leave += txtTitle_Leave;
            
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAuthor.Location = new Point(12, 55);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(47, 15);
            lblAuthor.TabIndex = 2;
            lblAuthor.Text = "Author:";
            
            // 
            // txtAuthor
            // 
            txtAuthor.Font = new Font("Segoe UI", 9F);
            txtAuthor.Location = new Point(115, 52);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(230, 23);
            txtAuthor.TabIndex = 3;
            txtAuthor.Leave += txtAuthor_Leave;
            
            // 
            // lblGenre
            // 
            lblGenre.AutoSize = true;
            lblGenre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGenre.Location = new Point(12, 90);
            lblGenre.Name = "lblGenre";
            lblGenre.Size = new Size(44, 15);
            lblGenre.TabIndex = 4;
            lblGenre.Text = "Genre:";
            
            // 
            // txtGenre
            // 
            txtGenre.Font = new Font("Segoe UI", 9F);
            txtGenre.Location = new Point(115, 87);
            txtGenre.Name = "txtGenre";
            txtGenre.Size = new Size(230, 23);
            txtGenre.TabIndex = 5;
            txtGenre.Leave += txtGenre_Leave;
            
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblYear.Location = new Point(12, 125);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(98, 15);
            lblYear.TabIndex = 6;
            lblYear.Text = "Publication Year:";
            
            // 
            // numYear
            // 
            numYear.Font = new Font("Segoe UI", 9F);
            numYear.Location = new Point(115, 122);
            numYear.Maximum = new decimal(new int[] { 2030, 0, 0, 0 });
            numYear.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numYear.Name = "numYear";
            numYear.Size = new Size(120, 23);
            numYear.TabIndex = 7;
            numYear.Value = new decimal(new int[] { DateTime.Now.Year, 0, 0, 0 });
            
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.LightGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(190, 250);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 8;
            btnSave.Text = "💾 Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.LightCoral;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Location = new Point(290, 250);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "❌ Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            
            // 
            // AddEditBookForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(384, 295);
            Controls.Add(pnlMain);
            Controls.Add(lblFormTitle);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddEditBookForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Book Management";
            ((System.ComponentModel.ISupportInitialize)numYear).EndInit();
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblAuthor;
        private TextBox txtAuthor;
        private Label lblGenre;
        private TextBox txtGenre;
        private Label lblYear;
        private NumericUpDown numYear;
        private Button btnSave;
        private Button btnCancel;
        private Panel pnlMain;
        private Label lblFormTitle;
    }
}