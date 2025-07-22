namespace fast_notes_app
{
    public partial class Form1 : Form
    {
        private string placeholderUsername = "Enter Username";
        private string placeholderPassword = "Enter Password";

        public Form1()
        {
            InitializeComponent();

            // Inicializar placeholders
            SetPlaceholder(textBox1, placeholderUsername);
            SetPlaceholder(textBox2, placeholderPassword);

            // Eventos para quitar y volver a poner placeholder
            textBox1.GotFocus += RemovePlaceholder;
            textBox1.LostFocus += AddPlaceholder;

            textBox2.GotFocus += RemovePlaceholder;
            textBox2.LostFocus += AddPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb == textBox1 && tb.Text == placeholderUsername)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
            else if (tb == textBox2 && tb.Text == placeholderPassword)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb == textBox1 && string.IsNullOrWhiteSpace(tb.Text))
            {
                SetPlaceholder(tb, placeholderUsername);
            }
            else if (tb == textBox2 && string.IsNullOrWhiteSpace(tb.Text))
            {
                SetPlaceholder(tb, placeholderPassword);
            }
        }

        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.Gray;
        }
    }
}
