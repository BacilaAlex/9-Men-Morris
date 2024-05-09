namespace Morris;

public partial class GameForm : Form
{
    GameMode gameMode;
    PictureBox boardPictureBox = new PictureBox();

    public GameForm(GameMode mode)
    {
        InitializeComponent();
        this.gameMode = mode;
    }

    private void GameForm_Load(object sender, EventArgs e)
    {
        var board = new Board(ClientSize.Width, ClientSize.Height, new Piece(), new Circle(), this);

        board.InitializeBoard();
        CenterForm();
    }

    private void CenterForm()
    {
        StartPosition = FormStartPosition.Manual;
        Location = new Point((Screen.PrimaryScreen.Bounds.Width - Width) / 2, (Screen.PrimaryScreen.Bounds.Height - Height) / 2);
    }

}
