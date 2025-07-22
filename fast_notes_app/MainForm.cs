using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fast_notes_app
{
    public partial class MainForm : Form
    {
        private UserInfo currentUser;
        private NotesManager notesManager;
        private List<NoteInfo> userNotes;
        private NoteInfo? currentNote;
        private bool isContentChanged = false;

        public MainForm(UserInfo user)
        {
            InitializeComponent();
            currentUser = user;
            notesManager = new NotesManager();
            userNotes = new List<NoteInfo>();


            userLabel.Text = $"Welcome, {currentUser.Username}";

            // Initialize
            InitializeAsync();
            SetupEventHandlers();
        }

        private async void InitializeAsync()
        {
            try
            {
                await LoadUserNotesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing: {ex.Message}");
            }
        }

        private void SetupEventHandlers()
        {
            createNoteButton.Click += CreateNoteButton_Click;

            notesListBox.SelectedIndexChanged += NotesListBox_SelectedIndexChanged;
            notesListBox.DrawItem += NotesListBox_DrawItem;

            saveButton.Click += SaveButton_Click;
            deleteButton.Click += DeleteButton_Click;

            logoutButton.Click += LogoutButton_Click;

            noteContentTextBox.TextChanged += (s, e) => isContentChanged = true;
            noteTitleTextBox.TextChanged += (s, e) => isContentChanged = true;
        }

        private async Task LoadUserNotesAsync()
        {
            try
            {
                userNotes = await notesManager.GetUserNotesAsync(currentUser.Id);

                notesListBox.Items.Clear();
                foreach (var note in userNotes)
                {
                    notesListBox.Items.Add(note);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notes: {ex.Message}");
            }
        }

        private async void CreateNoteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string title = $"New Note {DateTime.Now:MMdd HHmm}";
                var (success, message, noteId) = await notesManager.CreateNoteAsync(currentUser.Id, title);

                if (success)
                {
                    await LoadUserNotesAsync();

                    var newNote = userNotes.FirstOrDefault(n => n.Id == noteId);
                    if (newNote != null)
                    {
                        notesListBox.SelectedItem = newNote;
                    }

                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating note: {ex.Message}");
            }
        }

        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notesListBox.SelectedItem is NoteInfo selectedNote)
            {
                LoadNoteContent(selectedNote);
            }
        }

        private void LoadNoteContent(NoteInfo note)
        {
            currentNote = note;

            welcomeLabel.Visible = false;
            noteTitleTextBox.Visible = true;
            noteContentTextBox.Visible = true;
            saveButton.Visible = true;
            deleteButton.Visible = true;

            noteTitleTextBox.Text = note.Title;
            noteContentTextBox.Text = note.Content;

            isContentChanged = false;
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (currentNote == null) return;

            try
            {
                saveButton.Text = "Saving...";
                saveButton.Enabled = false;

                var (success, message) = await notesManager.UpdateNoteAsync(
                    currentNote.Id,
                    currentUser.Id,
                    noteTitleTextBox.Text.Trim(),
                    noteContentTextBox.Text);

                if (success)
                {
                    currentNote.Title = noteTitleTextBox.Text.Trim();
                    currentNote.Content = noteContentTextBox.Text;
                    isContentChanged = false;

                    await LoadUserNotesAsync();
                    notesListBox.SelectedItem = userNotes.FirstOrDefault(n => n.Id == currentNote.Id);
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving note: {ex.Message}");
            }
            finally
            {
                saveButton.Text = "Save";
                saveButton.Enabled = true;
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (currentNote == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{currentNote.Title}'?",
                "Delete Note",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var (success, message) = await notesManager.DeleteNoteAsync(currentNote.Id, currentUser.Id);

                    if (success)
                    {
                        await LoadUserNotesAsync();

                        welcomeLabel.Visible = true;
                        noteTitleTextBox.Visible = false;
                        noteContentTextBox.Visible = false;
                        saveButton.Visible = false;
                        deleteButton.Visible = false;

                        currentNote = null;

                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting note: {ex.Message}");
                }
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var loginForm = new Form1();
                loginForm.Show();
            }
        }

        private void NotesListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var note = (NoteInfo)notesListBox.Items[e.Index];

            Color bgColor = e.State.HasFlag(DrawItemState.Selected)
                ? Color.FromArgb(88, 101, 242)
                : Color.FromArgb(25, 25, 35);

            using (var brush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            using (var titleBrush = new SolidBrush(Color.White))
            using (var titleFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            {
                var titleRect = new Rectangle(e.Bounds.X + 10, e.Bounds.Y + 8, e.Bounds.Width - 20, 20);
                e.Graphics.DrawString(note.Title, titleFont, titleBrush, titleRect);
            }

            using (var dateBrush = new SolidBrush(Color.FromArgb(160, 160, 180)))
            using (var dateFont = new Font("Segoe UI", 9F))
            {
                var dateRect = new Rectangle(e.Bounds.X + 10, e.Bounds.Y + 32, e.Bounds.Width - 20, 15);
                string dateText = note.UpdatedAt.ToString("MMM dd, yyyy HH:mm");
                e.Graphics.DrawString(dateText, dateFont, dateBrush, dateRect);
            }
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
