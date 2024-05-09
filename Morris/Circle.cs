namespace Morris
{
    internal class Circle : PictureBox, ICircle
    {
        public void CircleClick(object sender, EventArgs e, ref PictureBox? selectedPictureBox)
        {
            var clickedCircle = sender as Circle;
            if (selectedPictureBox != null)
            {
                selectedPictureBox.Location = clickedCircle.Location;
                selectedPictureBox.BringToFront();
                //Controls.Remove(selectedPictureBox);
            }
        }
    }
}
