using System.Drawing;
using System.Windows.Forms;

namespace Morris;

internal class Board
{
    private PictureBox boardPictureBox = new PictureBox();

    // Board dimensions
    private int BoardWidth;
    private int BoardHeight;
    private IPiece _piece;
    private ICircle _circle;
    private Control _control;

    public Board(int BoardWidth, int BoardHeight, IPiece piece, ICircle circle, Control control)
    {
        this.BoardWidth = BoardWidth;
        this.BoardHeight = BoardHeight;
        this._piece = piece;
        _circle = circle;
        _control = control;
    }

    public void InitializeBoard()
    {
        CalculateSqueresMeasurement();

        boardPictureBox.Size = new Size(BoardWidth, BoardHeight);
        boardPictureBox.Paint += BoardPictureBox_Paint;

        DrawPieces();

        _control.Controls.Add(boardPictureBox);
    }

    private void BoardPictureBox_Paint(object sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        Pen pen = new Pen(Color.Black, 2);

        DrawSquares(g, pen);

        // Draw intersection points
        int intersectionSize = pictureBoxSize; // Size of the intersection points

        DrawLines(g, pen);

        DrawPoints(g, intersectionSize);
    }

    int outerSquareSize;
    int middleSquareSize;
    int innerSquareSize;

    int outerSquareX;
    int outerSquareY;
    int middleSquareX;
    int middleSquareY;
    int innerSquareX;
    int innerSquareY;

    int squareCenterX;
    int squareCenterY;

    int outerSquareLeft;
    int outerSquareRight;
    int outerSquareTop;
    int outerSquareBottom;

    int middleSquareLeft;
    int middleSquareRight;
    int middleSquareTop;
    int middleSquareBottom;

    int innerSquareLeft;
    int innerSquareRight;
    int innerSquareTop;
    int innerSquareBottom;

    private void CalculateSqueresMeasurement()
    {
        // Calculate sizes for concentric squares
        outerSquareSize = (int)(Math.Min(BoardWidth, BoardHeight) * 0.5);
        middleSquareSize = (int)(outerSquareSize * 0.5);
        innerSquareSize = (int)(middleSquareSize * 0.5);

        // Calculate positions for concentric squares
        outerSquareX = (BoardWidth - outerSquareSize) / 2;
        outerSquareY = (BoardHeight - outerSquareSize) / 2;
        middleSquareX = (BoardWidth - middleSquareSize) / 2;
        middleSquareY = (BoardHeight - middleSquareSize) / 2;
        innerSquareX = (BoardWidth - innerSquareSize) / 2;
        innerSquareY = (BoardHeight - innerSquareSize) / 2;

        // Draw cross lines in the middle square extended to the outer square
        squareCenterX = middleSquareX + middleSquareSize / 2;
        squareCenterY = middleSquareY + middleSquareSize / 2;

        // Draw intersection points
        outerSquareLeft = outerSquareX;
        outerSquareRight = outerSquareX + outerSquareSize;
        outerSquareTop = outerSquareY;
        outerSquareBottom = outerSquareY + outerSquareSize;

        middleSquareLeft = middleSquareX;
        middleSquareRight = middleSquareX + middleSquareSize;
        middleSquareTop = middleSquareY;
        middleSquareBottom = middleSquareY + middleSquareSize;

        innerSquareLeft = innerSquareX;
        innerSquareRight = innerSquareX + innerSquareSize;
        innerSquareTop = innerSquareY;
        innerSquareBottom = innerSquareY + innerSquareSize;
    }

    private void DrawSquares(Graphics g, Pen pen)
    {
        g.DrawRectangle(pen, outerSquareX, outerSquareY, outerSquareSize, outerSquareSize);

        g.DrawRectangle(pen, middleSquareX, middleSquareY, middleSquareSize, middleSquareSize);

        g.DrawRectangle(pen, innerSquareX, innerSquareY, innerSquareSize, innerSquareSize);
    }

    private void DrawPoints(Graphics g, int intersectionSize)
    {
        // Draw intersection points for outer square
        DrawIntersectionPoints(g, Brushes.Black, outerSquareX, outerSquareY, outerSquareSize, intersectionSize, squareCenterX, squareCenterY);

        // Draw intersection points for middle square
        DrawIntersectionPoints(g, Brushes.Black, middleSquareX, middleSquareY, middleSquareSize, intersectionSize, squareCenterX, squareCenterY);

        // Draw intersection points for inner square
        DrawIntersectionPoints(g, Brushes.Black, innerSquareX, innerSquareY, innerSquareSize, intersectionSize, squareCenterX, squareCenterY);
    }

    private void DrawLines(Graphics g, Pen pen)
    {
        //Draw the lines
        g.DrawLine(pen, outerSquareLeft, squareCenterY, innerSquareLeft, squareCenterY);
        g.DrawLine(pen, outerSquareRight, squareCenterY, innerSquareRight, squareCenterY);

        g.DrawLine(pen, squareCenterX, outerSquareTop, squareCenterX, innerSquareTop);
        g.DrawLine(pen, squareCenterX, outerSquareBottom, squareCenterX, innerSquareBottom);
    }


    int pictureBoxSize = 35;
    PictureBox? selectedPictureBox = null;

    private void DrawIntersectionPoints(Graphics g, Brush brush, int startX, int startY, int squareSize, int intersectionSize, int squareCenterX, int squareCenterY)
    {
        int[] intersectionPointsX = { startX, squareCenterX, startX + squareSize };
        int[] intersectionPointsY = { startY, squareCenterY, startY + squareSize };

        foreach (int x in intersectionPointsX)
        {
            foreach (int y in intersectionPointsY)
            {
                if (x == squareCenterX && y == squareCenterY)
                    continue;

                var pointPictureBox = new Circle
                {
                    Size = new Size(intersectionSize, intersectionSize),
                    Location = new Point(x - intersectionSize / 2, y - intersectionSize / 2)
                };


                pointPictureBox.Click += (sender, e) => _circle.CircleClick(sender, e, ref selectedPictureBox);
                pointPictureBox.Paint += (sender, e) => CirclePaint(sender, e, pointPictureBox);

                _control.Controls.Add(pointPictureBox);

                pointPictureBox.BringToFront(); 
            }
        }
    }

    private void CirclePaint(object sender, PaintEventArgs e, PictureBox circlePictureBox)
    {
        // Choose the color and size of the point
        e.Graphics.FillEllipse(Brushes.Black, circlePictureBox.Location.X, circlePictureBox.Location.Y, circlePictureBox.Width, circlePictureBox.Height);
    }

    private void DrawPieces()
    {
        // Calculate the size and spacing for the pieces
        int pieceSize = pictureBoxSize; // Adjust the size as needed
        int pieceSpacing = 50; // Adjust the spacing as needed

        // Calculate the starting position for the white pieces on the left
        int startXWhite = (BoardWidth / 8) - (pieceSize / 2);

        // Calculate the starting position for the black pieces on the right
        int startXBlack = (7 * BoardWidth / 8) - (pieceSize / 2);

        // Calculate the vertical center position for the pieces
        int centerY = (BoardHeight - 9 * pieceSpacing) / 2;

        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string whitePieceImagePath = Path.Combine(projectDirectory, "Pieces", "WhitePiece.jpg");
        string blackPieceImagePath = Path.Combine(projectDirectory, "Pieces", "BlackPiece.jpg");

        // Draw white pieces
        for (int i = 0; i < 9; i++)
        {
            int y = centerY + i * pieceSpacing;

            // Create PictureBox for white piece
            var whitePiecePictureBox = new Piece
            {
                BackColor = Color.Transparent,
                Size = new Size(pieceSize, pieceSize),
                Location = new Point(startXWhite, y - pieceSize / 2),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            if (File.Exists(whitePieceImagePath))
            {
                Image whitePieceImage = Image.FromFile(whitePieceImagePath);
                whitePiecePictureBox.Image = whitePieceImage;
            }
            else
            {
                whitePiecePictureBox.Image = null;
            }

            whitePiecePictureBox.Click += (sender, e) => _piece.PieceClick(sender, e, ref selectedPictureBox);


            _control.Controls.Add(whitePiecePictureBox);

            whitePiecePictureBox.BringToFront();
        }

        // Draw black pieces
        for (int i = 0; i < 9; i++)
        {
            int y = centerY + i * pieceSpacing;

            // Create PictureBox for black piece
            var blackPiecePictureBox = new Piece
            {
                BackColor = Color.Transparent,
                Size = new Size(pieceSize, pieceSize),
                Location = new Point(startXBlack, y - pieceSize / 2),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            if (File.Exists(blackPieceImagePath))
            {
                Image blackPieceImage = Image.FromFile(blackPieceImagePath);
                blackPiecePictureBox.Image = blackPieceImage;
            }
            else
            {
                blackPiecePictureBox.Image = null;
            }

            blackPiecePictureBox.Click += (sender, e) => _piece.PieceClick(sender, e, ref selectedPictureBox);

            _control.Controls.Add(blackPiecePictureBox);

            blackPiecePictureBox.BringToFront();
        }
    }

}
