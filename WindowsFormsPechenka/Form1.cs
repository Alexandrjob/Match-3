using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsPechenka
{
    public class Form
    {
        private int _width = 280;
        private int _height = 280;
        private int _sizeOfSides = 40;
        private int[,] Arrfigures = new int[7, 7];

        public Form()
        {
            InitializeComponent();
            this.Width = _width+100;
            this.Height = _height+37;
            MapGenerate();
            _ArrayGenerate();

            DrawCircle(40,40);
            DrawCircle(80,80);
        }
        private void MapGenerate()
        {
            for (int i = 0; i < _width / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * i);
                pic.Size = new Size(_width, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _height / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * i, 0);
                pic.Size = new Size(1, _height);
                this.Controls.Add(pic);
            }
        }

        private void _ArrayGenerate()
        {
            Random r = new Random(4);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Arrfigures[i, j] = r.Next(0, 4);
                    //Console.Write("{0}\t", Arrfigures[i, j]);
                }
                //Console.WriteLine("\n");
            }


        }

        private void DrawCircle(int x, int y)
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
            this.Controls.Add(pic);
        }
    }
}
