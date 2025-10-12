namespace BookCatalog.Forms
{
    partial class ReportForm
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
            txtReport = new TextBox();
            lblTitle = new Label();
            pnlTop = new Panel();
            btnExport = new Button();
            btnRefresh = new Button();
            
            pnlTop.SuspendLayout();
            SuspendLayout();
            
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.LightGray;
            pnlTop.Controls.Add(btnRefresh);
            pnlTop.Controls.Add(btnExport);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(684, 60);
            pnlTop.TabIndex = 1;
            
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Location = new Point(12, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📊 Book Catalog Statistics Report";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.LightBlue;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.Location = new Point(500, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 25);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = Color.LightGreen;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.Location = new Point(590, 18);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(80, 25);
            btnExport.TabIndex = 2;
            btnExport.Text = "💾 Export";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            
            // 
            // txtReport
            // 
            txtReport.BackColor = Color.White;
            txtReport.Dock = DockStyle.Fill;
            txtReport.Font = new Font("Consolas", 10F);
            txtReport.Location = new Point(0, 60);
            txtReport.Multiline = true;
            txtReport.Name = "txtReport";
            txtReport.ReadOnly = true;
            txtReport.ScrollBars = ScrollBars.Both;
            txtReport.Size = new Size(684, 401);
            txtReport.TabIndex = 0;
            
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(684, 461);
            Controls.Add(txtReport);
            Controls.Add(pnlTop);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(600, 400);
            Name = "ReportForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Book Catalog Report";
            pnlTop.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtReport;
        private Label lblTitle;
        private Panel pnlTop;
        private Button btnExport;
        private Button btnRefresh;
    }
}