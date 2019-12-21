using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsAppPechenka
{
    class Draw
    {
        static Playspace playspace = new Playspace();//Ссылачка на игровое пространство
        public static void Circle(int x, int y)
        {
            PictureBox pic = CreatPictureBox(x, y);

            Graphics graph = Graphics.FromImage(pic.Image);
            //graph.FillRectangle(Brushes.Black, pic.ClientRectangle);
            Pen pen = new Pen(Color.DimGray, 2);
            graph.DrawEllipse(pen, 5, 5, 30, 30);
            playspace.Controls.Add(pic);
            pic.Refresh();
        }

        public static void Rectangle(int x, int y)
        {
            PictureBox pic = CreatPictureBox(x, y);

            Graphics graph = Graphics.FromImage(pic.Image);
            Pen pen = new Pen(Color.Black, 2);
            graph.DrawRectangle(pen, 5, 5, 30, 30);
            playspace.Controls.Add(pic);
            pic.Refresh();
        }

        public static void Triangle(int x, int y)
        {
            PictureBox pic = CreatPictureBox(x, y);

            Graphics graph = Graphics.FromImage(pic.Image);
            Pen pen = new Pen(Color.Brown, 2);
            graph.DrawLine(pen, 5, 5, 35, 5);
            graph.DrawLine(pen, 5, 5, 20, 35);
            graph.DrawLine(pen, 35, 5, 20, 35);
            playspace.Controls.Add(pic);
            pic.Refresh();
        }
        public static void TriangleReverse(int x, int y)
        {
            PictureBox pic = CreatPictureBox(x, y);

            Graphics graph = Graphics.FromImage(pic.Image);
            Pen pen = new Pen(Color.Red, 2);
            graph.DrawLine(pen, 5, 35, 35, 35);
            graph.DrawLine(pen, 5, 35, 20, 5);
            graph.DrawLine(pen, 20, 5, 35, 35);
            playspace.Controls.Add(pic);
            pic.Refresh();
        }
        public static void X(int x, int y)
        {
            PictureBox pic = CreatPictureBox(x, y);

            Graphics graph = Graphics.FromImage(pic.Image);
            Pen pen = new Pen(Color.Green, 2);
            graph.DrawLine(pen, 5, 5, 35, 35);
            graph.DrawLine(pen, 5, 35, 35, 5);
            playspace.Controls.Add(pic);
            pic.Refresh();
        }

        private static PictureBox CreatPictureBox(int x, int y)
        {
            PictureBox pic = new PictureBox
            {
                Image = new Bitmap(40, 40),
                Location = new Point(x * 40, y * 40),
                Size = new Size(40, 40)
            };
            //Как работать с массивами в таком случае?
            PictureArrfigures[x, y] = pic;
            pic.Click += new EventHandler(PictureBox_Click);
            return pic;
        }

    }
}
