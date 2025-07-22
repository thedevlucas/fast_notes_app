namespace fast_notes_app
{
    partial class Form1
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            navPanel = new Panel();
            appNameLabel = new Label();
            closeButton = new Button();
            minimizeButton = new Button();
            SuspendLayout();

            // 
            // Form1 - Main Form
            // 
            this.BackColor = Color.FromArgb(15, 15, 23);
            this.ClientSize = new Size(600, 540); // Increased height for nav bar
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.Text = "Fast Notes";

            // Add rounded corners effect
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // 
            // navPanel - Navigation Bar
            // 
            navPanel.Size = new Size(600, 40);
            navPanel.Location = new Point(0, 0);
            navPanel.BackColor = Color.FromArgb(20, 20, 28);
            navPanel.MouseDown += Form1_MouseDown; // Make nav draggable

            // 
            // appNameLabel - App Name
            // 
            appNameLabel.AutoSize = true;
            appNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            appNameLabel.ForeColor = Color.FromArgb(200, 200, 220);
            appNameLabel.Text = "Fast Notes";
            appNameLabel.Location = new Point(20, 12);
            appNameLabel.BackColor = Color.Transparent;
            appNameLabel.MouseDown += Form1_MouseDown; // Make draggable

            // 
            // minimizeButton - Minimize Button
            // 
            minimizeButton.Size = new Size(30, 30);
            minimizeButton.Location = new Point(540, 5);
            minimizeButton.Text = "−";
            minimizeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            minimizeButton.FlatStyle = FlatStyle.Flat;
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.BackColor = Color.Transparent;
            minimizeButton.ForeColor = Color.FromArgb(160, 160, 180);
            minimizeButton.Cursor = Cursors.Hand;
            minimizeButton.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            // Hover effects for minimize
            minimizeButton.MouseEnter += (s, e) => {
                minimizeButton.BackColor = Color.FromArgb(40, 40, 50);
            };
            minimizeButton.MouseLeave += (s, e) => {
                minimizeButton.BackColor = Color.Transparent;
            };

            // 
            // closeButton - Close Button
            // 
            closeButton.Size = new Size(30, 30);
            closeButton.Location = new Point(570, 5);
            closeButton.Text = "×";
            closeButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.BackColor = Color.Transparent;
            closeButton.ForeColor = Color.FromArgb(160, 160, 180);
            closeButton.Cursor = Cursors.Hand;
            closeButton.Click += (s, e) => this.Close();

            closeButton.MouseEnter += (s, e) => {
                closeButton.BackColor = Color.FromArgb(220, 53, 69);
                closeButton.ForeColor = Color.White;
            };
            closeButton.MouseLeave += (s, e) => {
                closeButton.BackColor = Color.Transparent;
                closeButton.ForeColor = Color.FromArgb(160, 160, 180);
            };

            // PANEL1 - Main Content Panel
            panel1.Size = new Size(600, 500);
            panel1.Location = new Point(0, 40); // Moved down to accommodate nav bar
            panel1.BackColor = Color.FromArgb(15, 15, 23);
            panel1.Paint += Panel1_Paint; // Custom gradient paint

            //TITLE
            label1.AutoSize = false;
            label1.Size = new Size(400, 60);
            label1.Font = new Font("Segoe UI", 32F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Text = "Welcome Back";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Location = new Point((panel1.ClientSize.Width - label1.Width) / 2, 60); // Adjusted for nav bar
            label1.BackColor = Color.Transparent;

            //SUBTITLE
            label2.AutoSize = false;
            label2.Size = new Size(400, 30);
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(160, 160, 180);
            label2.Text = "Sign in to continue to Fast Notes";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Location = new Point((panel1.ClientSize.Width - label2.Width) / 2, 120);
            label2.BackColor = Color.Transparent;

            //USERNAME CONTAINER
            panel2.Size = new Size(320, 50);
            panel2.Location = new Point((panel1.ClientSize.Width - panel2.Width) / 2, 180);
            panel2.BackColor = Color.FromArgb(25, 25, 35);
            panel2.Paint += (s, e) => {
                using (var path = GetRoundedRectanglePath(panel2.ClientRectangle, 12))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var brush = new SolidBrush(Color.FromArgb(25, 25, 35)))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    using (var pen = new Pen(Color.FromArgb(45, 45, 55), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            //USERNAME TEXTBOX
            textBox1.Location = new Point(15, 12);
            textBox1.Size = new Size(290, 26);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 11F);
            textBox1.BackColor = Color.FromArgb(25, 25, 35);
            textBox1.ForeColor = Color.White;
            textBox1.Text = "Enter your username";

            //PASSWORD CONTAINER
            panel3.Size = new Size(320, 50);
            panel3.Location = new Point((panel1.ClientSize.Width - panel3.Width) / 2, 250);
            panel3.BackColor = Color.FromArgb(25, 25, 35);
            panel3.Paint += (s, e) => {
                using (var path = GetRoundedRectanglePath(panel3.ClientRectangle, 12))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var brush = new SolidBrush(Color.FromArgb(25, 25, 35)))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    using (var pen = new Pen(Color.FromArgb(45, 45, 55), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            //PASSWORD TEXTBOX
            textBox2.Location = new Point(15, 12);
            textBox2.Size = new Size(290, 26);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 11F);
            textBox2.BackColor = Color.FromArgb(25, 25, 35);
            textBox2.ForeColor = Color.White;
            textBox2.Text = "Enter your password";
            textBox2.UseSystemPasswordChar = false;

            //LOGIN BTN
            button1.Location = new Point((panel1.ClientSize.Width - 320) / 2, 330);
            button1.Size = new Size(320, 50);
            button1.Text = "Sign In";
            button1.Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.FromArgb(88, 101, 242);
            button1.ForeColor = Color.White;
            button1.Cursor = Cursors.Hand;
            button1.Paint += Button1_Paint;

            button1.MouseEnter += (s, e) => {
                button1.BackColor = Color.FromArgb(71, 82, 196);
                button1.Invalidate();
            };
            button1.MouseLeave += (s, e) => {
                button1.BackColor = Color.FromArgb(88, 101, 242);
                button1.Invalidate();
            };

            navPanel.Controls.Add(appNameLabel);
            navPanel.Controls.Add(minimizeButton);
            navPanel.Controls.Add(closeButton);

            panel2.Controls.Add(textBox1);
            panel3.Controls.Add(textBox2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(button1);

            this.Controls.Add(navPanel);
            this.Controls.Add(panel1);

            ResumeLayout(false);
        }

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label2;
        private Label label3;

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                panel1.ClientRectangle,
                Color.FromArgb(15, 15, 23),
                Color.FromArgb(25, 25, 40),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
            }
        }

        private void Button1_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            using (var path = GetRoundedRectanglePath(btn.ClientRectangle, 12))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }

            // Draw text
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Panel navPanel;
        private Label appNameLabel;
        private Button closeButton;
        private Button minimizeButton;
    }
}
