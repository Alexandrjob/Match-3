using System;
using System.Windows.Forms;

namespace WindowsFormsAppPechenka
{
    public class FigureDataBase
    {
        //Любимые массивы(В них хранятся фигурки и их номера)
        public PictureBox[,] PictureArrfigures = new PictureBox[8, 8];
        public int[,] NumberArrfigures = new int[8, 8];

        private readonly Random random = new Random();

        private readonly PlayForm PlayForm;

        public FigureDataBase(PlayForm playForm)
        {
            PlayForm = playForm;
        }
        public void ArraysGenerate()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    NumberArrfigures[i, j] = random.Next(1, 6);
                    FigureGenerate(j, i, NumberArrfigures[i, j]);
                }
            }
        }

        public void FigureGenerate(int i, int j, int value)
        {
            var picture = Draw.CreateFigure(value, i, j);
            picture.Click += new EventHandler(PlayForm.PictureBox_Click);
            PlayForm.Controls.Add(picture);
            PictureArrfigures[i, j] = picture;
            picture.Refresh();
        }
    }
}
