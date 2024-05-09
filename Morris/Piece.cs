namespace Morris
{
    public class Piece : PictureBox, IPiece
    {
        public void PieceClick(object sender, EventArgs e, ref PictureBox? selectedPictureBox)
        {
            var clickedPiece = sender as Piece;
            if (selectedPictureBox == null)
            {
                selectedPictureBox = clickedPiece;
            }
            else
            {
                selectedPictureBox.Location = clickedPiece.Location;
                selectedPictureBox.BringToFront();
                //Controls.Remove(selectedPictureBox);
            }
        }
    }
}
