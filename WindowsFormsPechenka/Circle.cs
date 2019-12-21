using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsPechenka
{
    public class Circle
    {
        public int Radius { get; set; }

        Graphics e;
        public Circle(int R)
        {
            Radius = R;
        }

        public void Draw(int x, int y)
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
            Controls.Add(pic);
        }
    }
}
