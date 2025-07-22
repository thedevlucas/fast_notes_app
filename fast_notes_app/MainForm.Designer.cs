using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fast_notes_app
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            navPanel = new Panel();
            appNameLabel = new Label();
            userLabel = new Label();
            logoutButton = new Button();
            closeButton = new Button();
            minimizeButton = new Button();
            mainPanel = new Panel();
            sidebarPanel = new Panel();
            createNoteButton = new Button();
            notesListBox = new ListBox();
            sidebarTitle = new Label();
            contentPanel = new Panel();
            noteContentTextBox = new RichTextBox();
            noteTitleTextBox = new TextBox();
            saveButton = new Button();
            deleteButton = new Button();
            welcomeLabel = new Label();
            SuspendLayout();

            // 
            // MainForm
            // 
            this.BackColor = Color.FromArgb(15, 15, 23);
            this.ClientSize = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.Text = "Fast Notes - Dashboard";

            // Add rounded corners
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // 
            // navPanel - Navigation Bar
            // 
            navPanel.Size = new Size(1200, 50);
            navPanel.Location = new Point(0, 0);
            navPanel.BackColor = Color.FromArgb(20, 20, 28);
            navPanel.MouseDown += MainForm_MouseDown;

            // 
            // appNameLabel - App Name
            // 
            appNameLabel.AutoSize = true;
            appNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            appNameLabel.ForeColor = Color.FromArgb(200, 200, 220);
            appNameLabel.Text = "Fast Notes";
            appNameLabel.Location = new Point(20, 15);
            appNameLabel.BackColor = Color.Transparent;
            appNameLabel.MouseDown += MainForm_MouseDown;

            // 
            // userLabel - User Info
            // 
            userLabel.AutoSize = true;
            userLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            userLabel.ForeColor = Color.FromArgb(160, 160, 180);
            userLabel.Text = "Welcome, User";
            userLabel.Location = new Point(150, 17);
            userLabel.BackColor = Color.Transparent;

            // 
            // logoutButton - Logout Button
            // 
            logoutButton.Size = new Size(70, 30);
            logoutButton.Location = new Point(1050, 10);
            logoutButton.Text = "Logout";
            logoutButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            logoutButton.FlatStyle = FlatStyle.Flat;
            logoutButton.FlatAppearance.BorderSize = 1;
            logoutButton.FlatAppearance.BorderColor = Color.FromArgb(88, 101, 242);
            logoutButton.BackColor = Color.Transparent;
            logoutButton.ForeColor = Color.FromArgb(88, 101, 242);
            logoutButton.Cursor = Cursors.Hand;

            logoutButton.MouseEnter += (s, e) => {
                logoutButton.BackColor = Color.FromArgb(88, 101, 242);
                logoutButton.ForeColor = Color.White;
            };
            logoutButton.MouseLeave += (s, e) => {
                logoutButton.BackColor = Color.Transparent;
                logoutButton.ForeColor = Color.FromArgb(88, 101, 242);
            };

            // 
            // minimizeButton - Minimize Button
            // 
            minimizeButton.Size = new Size(30, 30);
            minimizeButton.Location = new Point(1140, 10);
            minimizeButton.Text = "−";
            minimizeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            minimizeButton.FlatStyle = FlatStyle.Flat;
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.BackColor = Color.Transparent;
            minimizeButton.ForeColor = Color.FromArgb(160, 160, 180);
            minimizeButton.Cursor = Cursors.Hand;
            minimizeButton.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            minimizeButton.MouseEnter += (s, e) => minimizeButton.BackColor = Color.FromArgb(40, 40, 50);
            minimizeButton.MouseLeave += (s, e) => minimizeButton.BackColor = Color.Transparent;

            // 
            // closeButton - Close Button
            // 
            closeButton.Size = new Size(30, 30);
            closeButton.Location = new Point(1170, 10);
            closeButton.Text = "×";
            closeButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.BackColor = Color.Transparent;
            closeButton.ForeColor = Color.FromArgb(160, 160, 180);
            closeButton.Cursor = Cursors.Hand;
            closeButton.Click += (s, e) => Application.Exit();

            closeButton.MouseEnter += (s, e) => {
                closeButton.BackColor = Color.FromArgb(220, 53, 69);
                closeButton.ForeColor = Color.White;
            };
            closeButton.MouseLeave += (s, e) => {
                closeButton.BackColor = Color.Transparent;
                closeButton.ForeColor = Color.FromArgb(160, 160, 180);
            };

            // 
            // mainPanel - Main Content Area
            // 
            mainPanel.Size = new Size(1200, 650);
            mainPanel.Location = new Point(0, 50);
            mainPanel.BackColor = Color.FromArgb(15, 15, 23);

            // 
            // sidebarPanel - Sidebar for Notes List
            // 
            sidebarPanel.Size = new Size(300, 650);
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.BackColor = Color.FromArgb(20, 20, 28);

            // 
            // sidebarTitle - Sidebar Title
            // 
            sidebarTitle.AutoSize = false;
            sidebarTitle.Size = new Size(280, 40);
            sidebarTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            sidebarTitle.ForeColor = Color.White;
            sidebarTitle.Text = "My Notes";
            sidebarTitle.TextAlign = ContentAlignment.MiddleLeft;
            sidebarTitle.Location = new Point(20, 20);
            sidebarTitle.BackColor = Color.Transparent;

            // 
            // createNoteButton - Create New Note Button
            // 
            createNoteButton.Size = new Size(260, 45);
            createNoteButton.Location = new Point(20, 80);
            createNoteButton.Text = "+ Create New Note";
            createNoteButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            createNoteButton.FlatStyle = FlatStyle.Flat;
            createNoteButton.FlatAppearance.BorderSize = 0;
            createNoteButton.BackColor = Color.FromArgb(88, 101, 242);
            createNoteButton.ForeColor = Color.White;
            createNoteButton.Cursor = Cursors.Hand;

            createNoteButton.MouseEnter += (s, e) => createNoteButton.BackColor = Color.FromArgb(71, 82, 196);
            createNoteButton.MouseLeave += (s, e) => createNoteButton.BackColor = Color.FromArgb(88, 101, 242);

            // 
            // notesListBox - Notes List
            // 
            notesListBox.Size = new Size(260, 500);
            notesListBox.Location = new Point(20, 140);
            notesListBox.BackColor = Color.FromArgb(25, 25, 35);
            notesListBox.ForeColor = Color.White;
            notesListBox.BorderStyle = BorderStyle.None;
            notesListBox.Font = new Font("Segoe UI", 10F);
            notesListBox.DrawMode = DrawMode.OwnerDrawFixed;
            notesListBox.ItemHeight = 60;

            // 
            // contentPanel - Content Area
            // 
            contentPanel.Size = new Size(900, 650);
            contentPanel.Location = new Point(300, 0);
            contentPanel.BackColor = Color.FromArgb(15, 15, 23);

            // 
            // welcomeLabel - Welcome Message
            // 
            welcomeLabel.AutoSize = false;
            welcomeLabel.Size = new Size(800, 100);
            welcomeLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            welcomeLabel.ForeColor = Color.FromArgb(160, 160, 180);
            welcomeLabel.Text = "Select a note to start editing\nor create a new one";
            welcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            welcomeLabel.Location = new Point(50, 275);
            welcomeLabel.BackColor = Color.Transparent;

            // 
            // noteTitleTextBox - Note Title
            // 
            noteTitleTextBox.Size = new Size(800, 40);
            noteTitleTextBox.Location = new Point(50, 30);
            noteTitleTextBox.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            noteTitleTextBox.BackColor = Color.FromArgb(25, 25, 35);
            noteTitleTextBox.ForeColor = Color.White;
            noteTitleTextBox.BorderStyle = BorderStyle.None;
            noteTitleTextBox.Text = "Untitled Note";
            noteTitleTextBox.Visible = false;

            // 
            // noteContentTextBox - Note Content
            // 
            noteContentTextBox.Size = new Size(800, 500);
            noteContentTextBox.Location = new Point(50, 90);
            noteContentTextBox.Font = new Font("Segoe UI", 11F);
            noteContentTextBox.BackColor = Color.FromArgb(25, 25, 35);
            noteContentTextBox.ForeColor = Color.White;
            noteContentTextBox.BorderStyle = BorderStyle.None;
            noteContentTextBox.Text = "";
            noteContentTextBox.Visible = false;

            // 
            // saveButton - Save Button
            // 
            saveButton.Size = new Size(100, 35);
            saveButton.Location = new Point(650, 600);
            saveButton.Text = "Save";
            saveButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.BackColor = Color.FromArgb(34, 197, 94);
            saveButton.ForeColor = Color.White;
            saveButton.Cursor = Cursors.Hand;
            saveButton.Visible = false;

            // 
            // deleteButton - Delete Button
            // 
            deleteButton.Size = new Size(100, 35);
            deleteButton.Location = new Point(760, 600);
            deleteButton.Text = "Delete";
            deleteButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.BackColor = Color.FromArgb(239, 68, 68);
            deleteButton.ForeColor = Color.White;
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.Visible = false;

            // Add controls to their containers
            navPanel.Controls.Add(appNameLabel);
            navPanel.Controls.Add(userLabel);
            navPanel.Controls.Add(logoutButton);
            navPanel.Controls.Add(minimizeButton);
            navPanel.Controls.Add(closeButton);

            sidebarPanel.Controls.Add(sidebarTitle);
            sidebarPanel.Controls.Add(createNoteButton);
            sidebarPanel.Controls.Add(notesListBox);

            contentPanel.Controls.Add(welcomeLabel);
            contentPanel.Controls.Add(noteTitleTextBox);
            contentPanel.Controls.Add(noteContentTextBox);
            contentPanel.Controls.Add(saveButton);
            contentPanel.Controls.Add(deleteButton);

            mainPanel.Controls.Add(sidebarPanel);
            mainPanel.Controls.Add(contentPanel);

            this.Controls.Add(navPanel);
            this.Controls.Add(mainPanel);

            ResumeLayout(false);
        }

        // Import for rounded corners
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        #endregion

        private Panel navPanel;
        private Label appNameLabel;
        private Label userLabel;
        private Button logoutButton;
        private Button closeButton;
        private Button minimizeButton;
        private Panel mainPanel;
        private Panel sidebarPanel;
        private Button createNoteButton;
        private ListBox notesListBox;
        private Label sidebarTitle;
        private Panel contentPanel;
        private RichTextBox noteContentTextBox;
        private TextBox noteTitleTextBox;
        private Button saveButton;
        private Button deleteButton;
        private Label welcomeLabel;
    }
}
