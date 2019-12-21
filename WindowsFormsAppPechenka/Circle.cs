using System;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsAppPechenka
{
    public class Circle
    {
        public int Radius { get; set; }
        public Circle()
        {
           
        }

        public void Draw(int x, int y,Form f)
        {
            PictureBox pic = new PictureBox();
            pic.Image = new Bitmap(40, 40);
            pic.Location = new Point(x, y);
            pic.Size = new Size(40, 40);
            //pic.BackColor = Color.Black;

            Graphics graph = Graphics.FromImage(pic.Image);
            //graph.FillRectangle(Brushes.Black, pic.ClientRectangle);
            graph.DrawEllipse(Pens.Red, 5, 5, 30, 30);

            graph.Dispose();
            f.Controls.Add(pic);
        }
    }
}
