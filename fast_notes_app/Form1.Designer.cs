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
            SuspendLayout();
            // 
            // Form1
            // 
            this.BackColor = Color.FromArgb(18, 18, 18); // Fondo oscuro
            this.ClientSize = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.Text = "Fast Notes Login";

            // 
            // label1 - Título
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Text = "Sign In";
            label1.Location = new Point((this.ClientSize.Width - label1.PreferredWidth) / 2, 50);

            // 
            // textBox1 - Username
            // 
            textBox1.Location = new Point(140, 150);
            textBox1.Size = new Size(220, 35);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Segoe UI", 11F);
            textBox1.BackColor = Color.FromArgb(30, 30, 30); // Campo oscuro
            textBox1.ForeColor = Color.White;
            textBox1.PlaceholderText = "Enter Username"; // .NET 6+ soporta esto directamente

            // 
            // textBox2 - Password
            // 
            textBox2.Location = new Point(140, 200);
            textBox2.Size = new Size(220, 35);
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Segoe UI", 11F);
            textBox2.BackColor = Color.FromArgb(30, 30, 30);
            textBox2.ForeColor = Color.White;
            textBox2.PlaceholderText = "Enter Password";
            textBox2.UseSystemPasswordChar = true;

            // 
            // button1 - LOGIN
            // 
            button1.Location = new Point(140, 260);
            button1.Size = new Size(220, 45);
            button1.Text = "LOGIN";
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.FromArgb(88, 101, 242); // Azul morado vibrante
            button1.ForeColor = Color.White;
            button1.Cursor = Cursors.Hand;

            // Hover efecto
            button1.MouseEnter += (s, e) => button1.BackColor = Color.FromArgb(71, 82, 196);
            button1.MouseLeave += (s, e) => button1.BackColor = Color.FromArgb(88, 101, 242);

            // 
            // Agregar controles
            // 
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Controls.Add(button1);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
    }
}
