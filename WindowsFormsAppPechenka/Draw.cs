using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsAppPechenka
{
    class Draw
    {
        public static PictureBox CreateFigure(int figureType, int positionX, int positionY)
        {
            PictureBox pic = CreatPictureBox(positionX, positionY);
            Graphics graph = Graphics.FromImage(pic.Image);
            switch (figureType)
            {
                case 1:
                    X(graph);
                    break;
                case 2:
                    Circle(graph);
                    break;
                case 3:
                    Rectangle(graph);
                    break;
                case 4:
                    Triangle(graph);
                    break;
                case 5:
                    TriangleReverse(graph);
                    break;
            }

            return pic;
        }

        private static PictureBox CreatPictureBox(int x, int y)
        {
            PictureBox pic = new PictureBox
            {
                Image = new Bitmap(40, 40),
                Location = new Point(x * 40, y * 40),
                Size = new Size(40, 40)
            };

            return pic;
        }

        private static void Circle( Graphics graph)
        {
            Pen pen = new Pen(Color.DimGray, 2);
            graph.DrawEllipse(pen, 5, 5, 30, 30);
        }

        private static void Rectangle(Graphics graph)
        {
            Pen pen = new Pen(Color.Black, 2);
            graph.DrawRectangle(pen, 5, 5, 30, 30);
        }

        private static void Triangle(Graphics graph)
        {
            Pen pen = new Pen(Color.Brown, 2);
            graph.DrawLine(pen, 5, 5, 35, 5);
            graph.DrawLine(pen, 5, 5, 20, 35);
            graph.DrawLine(pen, 35, 5, 20, 35);
        }
        private static void TriangleReverse(Graphics graph)
        {
            Pen pen = new Pen(Color.Red, 2);
            graph.DrawLine(pen, 5, 35, 35, 35);
            graph.DrawLine(pen, 5, 35, 20, 5);
            graph.DrawLine(pen, 20, 5, 35, 35);
        }
        private static void X(Graphics graph)
        {
            Pen pen = new Pen(Color.Green, 2);
            graph.DrawLine(pen, 5, 5, 35, 35);
            graph.DrawLine(pen, 5, 35, 35, 5);
        }
    }
}
