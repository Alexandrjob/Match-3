using System;
using System.Drawing;
using System.Threading;
using Timers = System.Timers;
using System.Windows.Forms;

namespace WindowsFormsAppPechenka
{
    public partial class Playspace : Form
    {
        //Тронешь - пристрелю(Для размеров формы и _sizeOfSides для размера ячеек и фигурок)
        private readonly int _width = 320;
        private readonly int _height = 320;
        private readonly int _sizeOfSides = 40;
        //*****

        //Любимые массивы(В них хранятся фигурки и их номера)
        PictureBox[,] PictureArrfigures = new PictureBox[8, 8];
        int[,] NumberArrfigures = new int[8, 8];
        //*****

        //Для работы двух методов  CheckingForIdenticalElements и DeleteElements
        int[,] sameElements = new int[4, 8];//массив для номерков фигурок для их удаления
        private int countergorizontal = 0;
        private int countervertical = 0;
        bool @is;
        //*****

        //Можно удалить есть Pic2Position, celladd(ТОЛЬКО ПОМЕНЯЙ СНАЧАЛО)
        Point Onenumber;
        Point Twonumber;
        //*****

        //Для номеров ячеек, которые меняются местави(Для PictureBox_Click и ReversSwapElements)
        private Point celladd;
        Point Pic2Position;
        //*****

        //Для красивых фигурок, которые мы меняем местами
        PictureBox picturebox1 = null;
        PictureBox picturebox2 = null;
        //*****

        //Для первого Picturebox, который выделился(необходим для проверок и метода пузырька в PictureBox_Click)
        private PictureBox picBox = null;
        //*****

        //Для числа содержашегося в массиве NumberArrfigures
        private int cellNumArr = 0;
        //*****

        //Стандартный цвет всех PictureBox'ов (Нужно для визуального выделения при шелчке мыши)
        private readonly Color PicBackColor = Color.FromArgb(255, 240, 240, 240);

        Random random = new Random();//Это чтобы фигруки были всегда разные

        private int _gameSecondsLeft = 60;
        private const int GameSecondsLeftWhenResultStart = 58;

        int gamepoint = 0;

       // private readonly SynchronizationContext syncContext;
        private readonly Timers.Timer _timer;
        private readonly Form _mainForm;

        public Playspace(Form mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            this.Width = _width + 170;
            this.Height = _height + 35;
            MapGenerate();
            ArraysGenerate();
            //syncContext = SynchronizationContext.Current;

            _timer = new Timers.Timer
            {
                AutoReset = true, // Чтобы операции удаления перекрывались
                Interval = 1000,
                Enabled = true
            };
            _timer.Elapsed += TimerTick;
            _timer.Start();
        }
        private void MapGenerate()
        {
            for (int i = 0; i <= _width / _sizeOfSides; i++)
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

        private void ArraysGenerate()
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

        private void FigureGenerate(int i, int j, int value)
        {
            var picture = Draw.CreateFigure(value, i, j);
            picture.Click += new EventHandler(PictureBox_Click);
            this.Controls.Add(picture);
            PictureArrfigures[i, j] = picture;
            picture.Refresh();
        }

        void moving()
        {
            int counter;
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (NumberArrfigures[i, j] == 0)
                    {
                        counter = 0;
                        //Считает последовательно идущие пустые ячейки
                        while (NumberArrfigures[i - counter, j] == 0)
                        {
                            //Тут не считает элемент на пересечении двух линий нулей
                            counter++;
                            if (i - counter < 0)
                            {
                                break;
                            }
                        }
                        //Опускает/создает ячейки со значением на месте пустых
                        for (int h = 0; h < counter; h++)
                        {
                            InvokeMethod();
                            //Если ячейка 0-го ряда пуста, то генерируется элемент и опускается вниз
                            if (i - counter - h < 0)
                            {
                                for (int u = 0; u < counter; u++)
                                {
                                    if (counter > 4 && u == counter % 2)
                                    {
                                        InvokeMethod();
                                    }
                                    //В случае, когда опускать нечего, мы просто создаем элемент и останавливаем цикл
                                    CreatNewPictureBox(j);
                                    if (counter - 1 - u == 0)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(100);
                                    NumberArrfigures[i - h - u, j] = NumberArrfigures[0, j];
                                    NumberArrfigures[0, j] = 0;

                                    PictureBox picturebox = PictureArrfigures[j, 0];
                                    PictureArrfigures[j, 0] = null;
                                    picturebox.Location = new Point(j * 40, (i - h - u) * 40);
                                    Thread.Sleep(100);
                                    PictureArrfigures[j, i - h - u] = picturebox;
                                }
                                break;
                            }
                            //Опускает
                            else
                            {
                                if (NumberArrfigures[i - counter - h, j] == 0) continue;

                                NumberArrfigures[i - h, j] = NumberArrfigures[i - counter - h, j];
                                NumberArrfigures[i - counter - h, j] = 0;

                                PictureBox picturebox = PictureArrfigures[j, i - counter - h];
                                PictureArrfigures[j, i - counter - h] = null;

                                picturebox.Location = new Point(j * 40, (i - h) * 40);
                                Thread.Sleep(100);
                                PictureArrfigures[j, i - h] = picturebox;
                            }
                        }
                    }
                }
            }
        }

        void CreatNewPictureBox(int y = 0)
        {
            NumberArrfigures[0, y] = random.Next(1, 6);
            FigureGenerate(y, 0, NumberArrfigures[0, y]);
        }

        //Проверка на одинаковые элеметы
        private void CheckingForIdenticalElements()
        {
            @is = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {

                    CheckGorizontal(i, j);
                    CheckVertical(i, j);
                }
                //Обязательный код, при котором удаляются элементы граничащиеся с одним из краев
                if (countergorizontal >= 2)
                {
                    DeleteElements(countergorizontal, 0);
                }
                if (countervertical >= 2)
                {
                    DeleteElements(countervertical, 2);
                }
                Array.Clear(sameElements, 0, 32);
                countervertical = 0;
                countergorizontal = 0;
            }
        }

        void CheckGorizontal(int i, int j)
        {
            if (NumberArrfigures[i, j] == NumberArrfigures[i, j + 1] & NumberArrfigures[i, j] != 0)
            {
                if (countergorizontal == 0)
                {
                    sameElements[0, countergorizontal] = i;
                    sameElements[1, countergorizontal] = j;

                    sameElements[0, countergorizontal + 1] = i;
                    sameElements[1, countergorizontal + 1] = j + 1;
                }
                else
                {
                    sameElements[0, countergorizontal + 1] = i;
                    sameElements[1, countergorizontal + 1] = j + 1;
                }
                countergorizontal++;
            }
            else if (countergorizontal >= 2)
            {
                DeleteElements(countergorizontal, 0);
                Array.Clear(sameElements, 0, 16);
                countergorizontal = 0;
            }
            else
            {
                Array.Clear(sameElements, 0, 16);
                countergorizontal = 0;
            }
        }
        void CheckVertical(int i, int j)
        {
            if (NumberArrfigures[j, i] == NumberArrfigures[j + 1, i] & NumberArrfigures[j, i] != 0)
            {
                if (countervertical == 0)
                {
                    sameElements[2, countervertical] = j;
                    sameElements[3, countervertical] = i;

                    sameElements[2, countervertical + 1] = j + 1;
                    sameElements[3, countervertical + 1] = i;
                }
                else
                {
                    sameElements[2, countervertical + 1] = j + 1;
                    sameElements[3, countervertical + 1] = i;
                }
                countervertical++;
            }
            else if (countervertical >= 2)
            {
                DeleteElements(countervertical, 2);
                Array.Clear(sameElements, 16, 16);
                countervertical = 0;
            }
            else
            {
                Array.Clear(sameElements, 16, 16);
                countervertical = 0;
            }
        }

        void DeleteElements(int counter, int deletepoint)
        {
            for (int p = 0; p < counter + 1; p++)
            {
                gamepoint += 100;
                labelpoints.Text = gamepoint.ToString();
                labelpoints.Refresh();

                int i = sameElements[deletepoint, p];
                int j = sameElements[deletepoint + 1, p];
                NumberArrfigures[i, j] = 0;
                if (PictureArrfigures[j, i] != null)
                {

                    PictureArrfigures[j, i].BackColor = Color.Aqua;
                    PictureArrfigures[j, i].Refresh();
                    Thread.Sleep(80);
                    PictureArrfigures[j, i].Dispose();
                    PictureArrfigures[j, i] = null;
                }
            }
            @is = true;
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (SwapElements(sender))
            {
                CheckingForIdenticalElements();
                if (NumberArrfigures[Onenumber.Y / 40, Onenumber.X / 40] != 0 & NumberArrfigures[Twonumber.Y / 40, Twonumber.X / 40] != 0)
                {
                    Thread.Sleep(150);
                    ReversSwapElements();
                }
                picturebox1 = null;
                picturebox2 = null;
                Pic2Position = new Point(0, 0);
                celladd = new Point(0, 0);
                picBox = null;
                cellNumArr = 0;
                do
                {
                    moving();
                    CheckingForIdenticalElements();
                }
                while (@is);
            }
        }
        bool SwapElements(object sender)  //Метод меняющий местами 2 элемента
        {
            if (picBox == null)
            {
                picBox = (sender as PictureBox);
                celladd = picBox.Location;

                cellNumArr = NumberArrfigures[celladd.Y / 40, celladd.X / 40];

                (picBox as PictureBox).BackColor = Color.FromArgb(40, 0, 0, 0);
                return false;
            }
            else
            {
                //После проверки исключает варианты, где фигуры расположены не рядом друг с другом |(или) находятся в углу от первой фигуры |(или) клик проихошел по одной фигуре
                if ((Math.Abs(picBox.Location.X - (sender as PictureBox).Location.X) > 40 | Math.Abs(picBox.Location.Y - (sender as PictureBox).Location.Y) > 40) |
                    (Math.Abs(picBox.Location.Y - (sender as PictureBox).Location.Y) == 40 & Math.Abs(picBox.Location.X - (sender as PictureBox).Location.X) == 40) |
                    ((sender as PictureBox).Location == picBox.Location))
                {
                    picBox.BackColor = PicBackColor;
                    picBox = null;
                    return false;
                }
                else //Происходит замена в массивах NumberArrfigures и PictureArrfigures и замена Location у обоих PictureBox'ов 
                {
                    Pic2Position = (sender as PictureBox).Location;

                    Onenumber = new Point(celladd.X, celladd.Y);
                    Twonumber = new Point(Pic2Position.X, Pic2Position.Y);

                    NumberArrfigures[celladd.Y / 40, celladd.X / 40] = NumberArrfigures[Pic2Position.Y / 40, Pic2Position.X / 40];
                    NumberArrfigures[Pic2Position.Y / 40, Pic2Position.X / 40] = cellNumArr;

                    picturebox1 = PictureArrfigures[celladd.X / 40, celladd.Y / 40];
                    picturebox2 = PictureArrfigures[Pic2Position.X / 40, Pic2Position.Y / 40];
                    picturebox1.Location = new Point(Pic2Position.X, Pic2Position.Y);
                    picturebox2.Location = new Point(celladd.X, celladd.Y);

                    PictureArrfigures[Pic2Position.X / 40, Pic2Position.Y / 40] = picturebox1;
                    PictureArrfigures[celladd.X / 40, celladd.Y / 40] = picturebox2;

                    picBox.BackColor = PicBackColor;
                    PictureArrfigures[Pic2Position.X / 40, Pic2Position.Y / 40].Refresh();
                    PictureArrfigures[celladd.X / 40, celladd.Y / 40].Refresh();
                    return true;
                }
            }
        }

        void ReversSwapElements()
        {
            cellNumArr = NumberArrfigures[celladd.Y / 40, celladd.X / 40];
            NumberArrfigures[celladd.Y / 40, celladd.X / 40] = NumberArrfigures[Pic2Position.Y / 40, Pic2Position.X / 40];
            NumberArrfigures[Pic2Position.Y / 40, Pic2Position.X / 40] = cellNumArr;

            picturebox1.Location = new Point(celladd.X, celladd.Y);
            picturebox2.Location = new Point(Pic2Position.X, Pic2Position.Y);
            PictureArrfigures[Pic2Position.X / 40, Pic2Position.Y / 40] = picturebox2;
            PictureArrfigures[celladd.X / 40, celladd.Y / 40] = picturebox1;
            PictureArrfigures[Pic2Position.X / 40, Pic2Position.Y / 40].Refresh();
            PictureArrfigures[celladd.X / 40, celladd.Y / 40].Refresh();
        }

        public delegate void InvokeDelegate();

        private void TimerTick(Object sourse, Timers.ElapsedEventArgs e)
        {
            labelTime.BeginInvoke(new InvokeDelegate(InvokeMethod));

            if (_gameSecondsLeft == GameSecondsLeftWhenResultStart)
            {
                _timer.Stop();
                _timer.Dispose();
                this.BeginInvoke(new InvokeDelegate(InvokeShowResult));
            }
            else _gameSecondsLeft--;
        }

        public void InvokeMethod()
        {
            labelTime.Text = _gameSecondsLeft.ToString();
            labelTime.Refresh();
        }

        public void InvokeShowResult()
        {
            ResultForm ResultForm = new ResultForm(_mainForm, this,gamepoint);
            ResultForm.ShowDialog();
        }

        private void Playspace_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosePlayspace(false);
        }

        private void ClosePlayspace(bool isButtonClose = true)
        {
            _timer.Stop();
            _timer.Dispose();
            if (isButtonClose)
            {
                this.Close();
                this.Dispose();
            }
            _mainForm.Visible = true;
        }

        private void ExitToMainFormBotton(object sender, EventArgs e)
        {
            ClosePlayspace();
        }
    }
}
