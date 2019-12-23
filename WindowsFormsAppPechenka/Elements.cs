using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsAppPechenka
{
    public class Elements
    {
        //Для номеров ячеек, которые меняются местави(Для PictureBox_Click и ReversSwapElements)
        private Point firstlocationfigure;
        private Point secondlocationfigure;

        //Для красивых фигурок, которые мы меняем местами
        private PictureBox picturebox1;
        private PictureBox picturebox2;

        //Для первого Picturebox, который выделился(необходим для проверок и метода пузырька в PictureBox_Click)
        private PictureBox firstcelectedfigure;

        //Для числа содержашегося в массиве NumberArrfigures
        private int valuearrayfirstfigure;

        private readonly PlayForm PlayForm;
        //private readonly InteractionWitchFigure Figure;
        
        //Стандартный цвет всех PictureBox'ов (Нужно для визуального выделения при шелчке мыши)
        private readonly Color PicBackColor = Color.FromArgb(255, 240, 240, 240);
        public Elements(PlayForm playForm)
        {
            PlayForm = playForm;
        }
        public void bla(object sender)
        {
            if (SwapElements(sender))
            {
                InteractionWitchFigure.CheckingForIdenticalElements();
                if (PlayForm.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40] != 0 & PlayForm.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40] != 0)
                {
                    Thread.Sleep(150);
                    ReversSwapElements();
                }
                picturebox1 = null;
                picturebox2 = null;
                firstlocationfigure = new Point(0, 0);
                secondlocationfigure = new Point(0, 0);
                firstcelectedfigure = null;
                valuearrayfirstfigure = 0;
                do
                {
                    PlayForm.moving();
                    InteractionWitchFigure.CheckingForIdenticalElements();
                }
                while (InteractionWitchFigure.isfiguresdelete);
            }
        }
        bool SwapElements(object sender)  //Метод меняющий местами 2 элемента
        {
            if (firstcelectedfigure == null)
            {
                firstcelectedfigure = (sender as PictureBox);
                firstlocationfigure = firstcelectedfigure.Location;

                valuearrayfirstfigure = PlayForm.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40];
                firstcelectedfigure.BackColor = Color.FromArgb(40, 0, 0, 0);
                return false;
            }
            else
            {
                //После проверки исключает варианты, где фигуры расположены не рядом друг с другом |(или) находятся в углу от первой фигуры |(или) клик проихошел по одной фигуре
                if ((Math.Abs(firstcelectedfigure.Location.X - (sender as PictureBox).Location.X) > 40 | Math.Abs(firstcelectedfigure.Location.Y - (sender as PictureBox).Location.Y) > 40) |
                    (Math.Abs(firstcelectedfigure.Location.Y - (sender as PictureBox).Location.Y) == 40 & Math.Abs(firstcelectedfigure.Location.X - (sender as PictureBox).Location.X) == 40) |
                    ((sender as PictureBox).Location == firstcelectedfigure.Location))
                {
                    firstcelectedfigure.BackColor = PicBackColor;
                    firstcelectedfigure = null;
                    return false;
                }
                else //Происходит замена в массивах NumberArrfigures и PictureArrfigures и замена Location у обоих PictureBox'ов 
                {
                    secondlocationfigure = (sender as PictureBox).Location;

                    picturebox1 = PlayForm.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40];
                    picturebox2 = PlayForm.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40];

                    picturebox1.Location = new Point(secondlocationfigure.X, secondlocationfigure.Y);
                    picturebox2.Location = new Point(firstlocationfigure.X, firstlocationfigure.Y);

                    PlayForm.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40] = PlayForm.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40];
                    PlayForm.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40] = valuearrayfirstfigure;

                    PlayForm.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40] = picturebox2;
                    PlayForm.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40] = picturebox1;

                    firstcelectedfigure.BackColor = PicBackColor;
                    PlayForm.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40].Refresh();
                    PlayForm.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40].Refresh();
                    return true;
                }
            }
        }

        void ReversSwapElements()
        {
            //Когда уже элементы перемешены, это оказывается 2 элемент
            valuearrayfirstfigure = PlayForm.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40];

            picturebox1.Location = new Point(firstlocationfigure.X, firstlocationfigure.Y);
            picturebox2.Location = new Point(secondlocationfigure.X, secondlocationfigure.Y);

            PlayForm.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40] = PlayForm.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40];
            PlayForm.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40] = valuearrayfirstfigure;

            PlayForm.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40] = picturebox1;
            PlayForm.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40] = picturebox2;

            PlayForm.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40].Refresh();
            PlayForm.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40].Refresh();
        }

    }
}
