using System.Runtime.CompilerServices;

namespace Morris
{
    public enum GameMode
    {
        PlayerVsPlayer,
        PlayerVsAI,
        AIVsAI
    }

    public partial class GameForm : Form
    {
        private GameMode gameMode;
        private PictureBox boardPictureBox = new PictureBox();

        // Board dimensions
        private int BoardWidth;
        private int BoardHeight;


        public GameForm(GameMode mode)
        {
            InitializeComponent();
            this.gameMode = mode;

            InitializeBoard();
        }


        private void InitializeBoard()
        {
            BoardWidth = ClientSize.Width;
            BoardHeight = ClientSize.Height;

            // Set up boardPictureBox
            boardPictureBox.Size = new Size(BoardWidth, BoardHeight);
            boardPictureBox.Location = new Point((ClientSize.Width - BoardWidth) / 2, (ClientSize.Height - BoardHeight) / 2);
            boardPictureBox.Paint += BoardPictureBox_Paint;

            // Add boardPictureBox to the form's controls
            Controls.Add(boardPictureBox);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            // Center the form on the screen
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                                      (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void BoardPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);

            // Calculate sizes for concentric squares
            int outerSquareSize = (int)(Math.Min(BoardWidth, BoardHeight) * 0.5);
            int middleSquareSize = (int)(outerSquareSize * 0.5);
            int innerSquareSize = (int)(middleSquareSize * 0.5);

            // Calculate positions for concentric squares
            int outerSquareX = (BoardWidth - outerSquareSize) / 2;
            int outerSquareY = (BoardHeight - outerSquareSize) / 2;
            int middleSquareX = (BoardWidth - middleSquareSize) / 2;
            int middleSquareY = (BoardHeight - middleSquareSize) / 2;
            int innerSquareX = (BoardWidth - innerSquareSize) / 2;
            int innerSquareY = (BoardHeight - innerSquareSize) / 2;

            // Draw outer square
            g.DrawRectangle(pen, outerSquareX, outerSquareY, outerSquareSize, outerSquareSize);

            // Draw middle square
            g.DrawRectangle(pen, middleSquareX, middleSquareY, middleSquareSize, middleSquareSize);

            // Draw inner square
            g.DrawRectangle(pen, innerSquareX, innerSquareY, innerSquareSize, innerSquareSize);

            // Draw cross lines in the middle square extended to the outer square
            int squareCenterX = middleSquareX + middleSquareSize / 2;
            int squareCenterY = middleSquareY + middleSquareSize / 2;

            // Draw intersection points
            int intersectionSize = 10; // Size of the intersection points

            // Draw intersection points
            int outerSquareLeft = outerSquareX;
            int outerSquareRight = outerSquareX + outerSquareSize;
            int outerSquareTop = outerSquareY;
            int outerSquareBottom = outerSquareY + outerSquareSize;

            int middleSquareLeft = middleSquareX;
            int middleSquareRight = middleSquareX + middleSquareSize;
            int middleSquareTop = middleSquareY;
            int middleSquareBottom = middleSquareY + middleSquareSize;

            int innerSquareLeft = innerSquareX;
            int innerSquareRight = innerSquareX + innerSquareSize;
            int innerSquareTop = innerSquareY;
            int innerSquareBottom = innerSquareY + innerSquareSize;

            //Draw the lines
            g.DrawLine(pen, outerSquareLeft, squareCenterY, innerSquareLeft, squareCenterY);
            g.DrawLine(pen, outerSquareRight, squareCenterY, innerSquareRight, squareCenterY);

            g.DrawLine(pen, squareCenterX, outerSquareTop, squareCenterX, innerSquareTop);
            g.DrawLine(pen, squareCenterX, outerSquareBottom, squareCenterX, innerSquareBottom);

            // Draw intersection points for outer square
            DrawIntersectionPoints(g, Brushes.Black, outerSquareX, outerSquareY, outerSquareSize, intersectionSize, squareCenterX, squareCenterY);

            // Draw intersection points for middle square
            DrawIntersectionPoints(g, Brushes.Black, middleSquareX, middleSquareY, middleSquareSize, intersectionSize, squareCenterX, squareCenterY);

            // Draw intersection points for inner square
            DrawIntersectionPoints(g, Brushes.Black, innerSquareX, innerSquareY, innerSquareSize, intersectionSize, squareCenterX, squareCenterY);

            DrawPieces();
        }

        private void DrawIntersectionPoints(Graphics g, Brush brush, int startX, int startY, int squareSize, int intersectionSize, int squareCenterX, int squareCenterY)
        {
            int[] intersectionPointsX = { startX, startX + squareSize, squareCenterX };
            int[] intersectionPointsY = { startY, startY + squareSize, squareCenterY };

            foreach (int x in intersectionPointsX)
            {
                foreach (int y in intersectionPointsY)
                {
                    if (x == squareCenterX && y == squareCenterY)
                        continue;
                    PictureBox pointPictureBox = new PictureBox();
                    pointPictureBox.BackColor = Color.Black; // Set transparent background
                    pointPictureBox.Size = new Size(intersectionSize, intersectionSize);
                    pointPictureBox.Location = new Point(x - intersectionSize / 2, y - intersectionSize / 2);
                    Controls.Add(pointPictureBox);
                    pointPictureBox.BringToFront(); // Ensure pointPictureBox is drawn above other controls
                    pointPictureBox.Click += BoardPictureBox_Click;
                    g.FillEllipse(brush, x - intersectionSize / 2, y - intersectionSize / 2, intersectionSize, intersectionSize);
                }
            }
        }

        private void DrawPieces()
        {
            // Calculate the size and spacing for the pieces
            int pieceSize = 35; // Adjust the size as needed
            int pieceSpacing = 50; // Adjust the spacing as needed

            // Calculate the starting position for the white pieces on the left
            int startXWhite = (BoardWidth / 8) - (pieceSize / 2);

            // Calculate the starting position for the black pieces on the right
            int startXBlack = (7 * BoardWidth / 8) - (pieceSize / 2);

            // Calculate the vertical center position for the pieces
            int centerY = (BoardHeight - 9 * pieceSpacing) / 2;

            // Draw white pieces
            for (int i = 0; i < 9; i++)
            {
                int y = centerY + i * pieceSpacing;

                // Create PictureBox for white piece
                PictureBox whitePiecePictureBox = new PictureBox();
                whitePiecePictureBox.BackColor = Color.Transparent;
                whitePiecePictureBox.Size = new Size(pieceSize, pieceSize);
                whitePiecePictureBox.Location = new Point(startXWhite, y - pieceSize / 2);
                whitePiecePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                whitePiecePictureBox.Click += PiecePictureBox_Click;

                string whitePieceImagePath = @"D:\Visual Studio Saves\9_Men_Morris\Morris\Morris\Pieces\WhitePiece.jpg"; // Update with your image path
                if (File.Exists(whitePieceImagePath))
                {
                    Image whitePieceImage = Image.FromFile(whitePieceImagePath);
                    whitePiecePictureBox.Image = whitePieceImage;
                }
                else
                {
                    whitePiecePictureBox.Image = null;
                }

                Controls.Add(whitePiecePictureBox);
                whitePiecePictureBox.BringToFront(); // Ensure PictureBox is drawn above other controls
            }

            // Draw black pieces
            for (int i = 0; i < 9; i++)
            {
                int y = centerY + i * pieceSpacing;

                // Create PictureBox for black piece
                PictureBox blackPiecePictureBox = new PictureBox();
                blackPiecePictureBox.BackColor = Color.Transparent;
                blackPiecePictureBox.Size = new Size(pieceSize, pieceSize);
                blackPiecePictureBox.Location = new Point(startXBlack, y - pieceSize / 2);
                blackPiecePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                blackPiecePictureBox.Click += PiecePictureBox_Click;


                string blackPieceImagePath = @"D:\Visual Studio Saves\9_Men_Morris\Morris\Morris\Pieces\BlackPiece.jpg"; // Update with your image path
                if (File.Exists(blackPieceImagePath))
                {
                    Image blackPieceImage = Image.FromFile(blackPieceImagePath);
                    blackPiecePictureBox.Image = blackPieceImage;
                }
                else
                {
                    blackPiecePictureBox.Image = null;
                }

                Controls.Add(blackPiecePictureBox);
                blackPiecePictureBox.BringToFront(); // Ensure PictureBox is drawn above other controls
            }
        }


        private PictureBox selectedPiecePictureBox = null;
        private PictureBox destinationPictureBox = null;

        private void PiecePictureBox_Click(object sender, EventArgs e)
        {
            selectedPiecePictureBox = sender as PictureBox;
        }

        private void BoardPictureBox_Click(object sender, EventArgs e)
        {
            if (selectedPiecePictureBox != null)
            {
                PictureBox destinationPictureBox = sender as PictureBox;
                if (destinationPictureBox != null)
                {
                    // Move the piece to the destination PictureBox
                    destinationPictureBox.Image = selectedPiecePictureBox.Image;
                    selectedPiecePictureBox.BringToFront();

                    selectedPiecePictureBox.Image = null;

                    // Reset selected piece
                    selectedPiecePictureBox = null;
                }
            }
        }

    }
}
