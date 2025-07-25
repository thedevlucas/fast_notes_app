namespace fast_notes_app
{
    public partial class Form1 : Form
    {
        private string placeholderUsername = "Enter your username";
        private string placeholderPassword = "Enter your password";
        private bool isUsernameEmpty = true;
        private bool isPasswordEmpty = true;
        private DatabaseHelper dbHelper;

        public Form1()
        {

            InitializeComponent();
            dbHelper = new DatabaseHelper();


            this.Shown += async (s, e) =>
            {
                this.Opacity = 0; // Hide the form visually

                await InitializeDatabaseAsync();
                await Task.Delay(300); // Small delay for smoothness

                // Only call this once - after database initialization
                await CheckSavedCredentialsAsync();

                // Only show if auto-login didn't succeed
                if (this.Visible)
                {
                    this.Opacity = 1;
                }
            };

            SetPlaceholderStyle(textBox1, true);
            SetPlaceholderStyle(textBox2, true);

            textBox1.GotFocus += (s, e) => {
                if (isUsernameEmpty)
                {
                    textBox1.Text = "";
                    SetPlaceholderStyle(textBox1, false);
                    isUsernameEmpty = false;
                }
            };

            textBox1.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    textBox1.Text = placeholderUsername;
                    SetPlaceholderStyle(textBox1, true);
                    isUsernameEmpty = true;
                }
            };

            textBox2.GotFocus += (s, e) => {
                if (isPasswordEmpty)
                {
                    textBox2.Text = "";
                    textBox2.UseSystemPasswordChar = true;
                    SetPlaceholderStyle(textBox2, false);
                    isPasswordEmpty = false;
                }
            };

            textBox2.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    textBox2.Text = placeholderPassword;
                    SetPlaceholderStyle(textBox2, true);
                    isPasswordEmpty = true;
                }
            };

            button1.Click += Button1_Click;

            this.MouseDown += Form1_MouseDown;
            panel1.MouseDown += Form1_MouseDown;
            label1.MouseDown += Form1_MouseDown;
            label2.MouseDown += Form1_MouseDown;

            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
                if (e.KeyCode == Keys.Enter)
                    button1.PerformClick();
            };
        }

        private async Task CheckSavedCredentialsAsync()
        {
            try
            {
                if (CredentialManager.HasSavedCredentials())
                {
                    var (success, username, userId) = CredentialManager.GetSavedCredentials();

                    if (success && !string.IsNullOrEmpty(username) && userId > 0)
                    {
                        var (loginSuccess, message, user) = await dbHelper.ValidateUserByIdAsync(userId, username);

                        if (loginSuccess && user != null)
                        {
                            this.Hide();
                            var mainForm = new MainForm(user);
                            mainForm.FormClosed += (s, e) => this.Close();
                            mainForm.Show();
                            return;
                        }
                        else
                        {
                            CredentialManager.DeleteSavedCredentials();
                        }

                        button1.Text = "Sign In";
                        button1.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auto-login error: {ex.Message}");
                button1.Text = "Sign In";
                button1.Enabled = true;
            }
        }

        private async Task InitializeDatabaseAsync()
        {
            try
            {
                bool connected = await dbHelper.TestConnectionAsync();
                if (!connected)
                {
                    MessageBox.Show("Failed to connect to database. Please check your connection.",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization failed: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SetPlaceholderStyle(TextBox textBox, bool isPlaceholder)
        {
            if (isPlaceholder)
            {
                textBox.ForeColor = Color.FromArgb(120, 120, 140);
            }
            else
            {
                textBox.ForeColor = Color.White;
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string username = isUsernameEmpty ? "" : textBox1.Text.Trim();
            string password = isPasswordEmpty ? "" : textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button1.Enabled = false;
            button1.Text = "Signing In...";

            try
            {
                var (success, message, user) = await dbHelper.LoginUserAsync(username, password);

                if (success && user != null)
                {
                    bool credentialsSaved = CredentialManager.SaveCredentials(user.Username, user.Id);
                    if (!credentialsSaved)
                    {
                        Console.WriteLine("Warning: Could not save login credentials for auto-login");
                    }

                    this.Hide();
                    var mainForm = new MainForm(user);
                    mainForm.FormClosed += (s, e) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show(message, "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
                button1.Text = "Sign In";
            }
        }

        // Make form draggable
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
