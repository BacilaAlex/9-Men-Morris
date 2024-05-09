namespace Morris
{
    internal interface IPiece
    {
        void PieceClick(object sender, EventArgs e, ref PictureBox? selectedPictureBox);
    }
}
