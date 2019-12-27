using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsAppPechenka
{
    public class Element
    {
        //Для номеров ячеек, которые меняются местави
        private Point firstlocationfigure;
        private Point secondlocationfigure;

        //Для красивых фигурок, которые мы меняем местами
        private PictureBox picturebox1;
        private PictureBox picturebox2;

        //Для первого Picturebox, который выделился
        private PictureBox firstcelectedfigure;

        //Для числа содержашегося в массиве NumberArrfigures
        private int valuearrayfirstfigure;

        //Стандартный цвет всех PictureBox'ов (Нужно для визуального выделения при шелчке мыши)
        private readonly Color PicBackColor = Color.FromArgb(255, 240, 240, 240);

        private readonly InteractionWitchFigure Interaction;
        private readonly PlayForm PlayForm;
        private readonly FigureDataBase FigureDataBase;

        public Element(PlayForm playForm, FigureDataBase figureDataBase)
        {
            PlayForm = playForm;
            FigureDataBase = figureDataBase;
            Interaction = new InteractionWitchFigure(figureDataBase);
        }

        public void Click(object sender)
        {
            if (SwapElements(sender))
            {
                Interaction.CheckingForIdenticalElements();
                if (FigureDataBase.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40] != 0 & FigureDataBase.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40] != 0)
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
                    Interaction.CheckingForIdenticalElements();
                }
                while (Interaction.isfiguresdelete);
            }
        }
        private bool SwapElements(object sender)  //Метод меняющий местами 2 элемента
        {
            if (firstcelectedfigure == null)
            {
                firstcelectedfigure = (sender as PictureBox);
                firstlocationfigure = firstcelectedfigure.Location;

                valuearrayfirstfigure = FigureDataBase.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40];
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

                    picturebox1 = FigureDataBase.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40];
                    picturebox2 = FigureDataBase.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40];

                    picturebox1.Location = new Point(secondlocationfigure.X, secondlocationfigure.Y);
                    picturebox2.Location = new Point(firstlocationfigure.X, firstlocationfigure.Y);

                    FigureDataBase.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40] = FigureDataBase.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40];
                    FigureDataBase.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40] = valuearrayfirstfigure;

                    FigureDataBase.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40] = picturebox2;
                    FigureDataBase.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40] = picturebox1;

                    firstcelectedfigure.BackColor = PicBackColor;
                    FigureDataBase.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40].Refresh();
                    FigureDataBase.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40].Refresh();
                    return true;
                }
            }
        }

        private void ReversSwapElements()
        {
            //Когда уже элементы перемешены, это оказывается 2 элемент
            valuearrayfirstfigure = FigureDataBase.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40];

            picturebox1.Location = new Point(firstlocationfigure.X, firstlocationfigure.Y);
            picturebox2.Location = new Point(secondlocationfigure.X, secondlocationfigure.Y);

            FigureDataBase.NumberArrfigures[firstlocationfigure.Y / 40, firstlocationfigure.X / 40] = FigureDataBase.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40];
            FigureDataBase.NumberArrfigures[secondlocationfigure.Y / 40, secondlocationfigure.X / 40] = valuearrayfirstfigure;

            FigureDataBase.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40] = picturebox1;
            FigureDataBase.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40] = picturebox2;

            FigureDataBase.PictureArrfigures[secondlocationfigure.X / 40, secondlocationfigure.Y / 40].Refresh();
            FigureDataBase.PictureArrfigures[firstlocationfigure.X / 40, firstlocationfigure.Y / 40].Refresh();
        }

    }
}
