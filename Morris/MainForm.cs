namespace Morris
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void btnPlayerVsPlayer_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(GameMode.PlayerVsPlayer);
            gameForm.Show();
            this.Hide(); // Optionally hide the main form
        }

        private void btnPlayerVsAI_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(GameMode.PlayerVsAI);
            gameForm.Show();
            this.Hide(); // Optionally hide the main form
        }

        private void btnAIVsAI_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(GameMode.AIVsAI);
            gameForm.Show();
            this.Hide(); // Optionally hide the main form
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Center the form on the screen
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                                      (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }
    }
}