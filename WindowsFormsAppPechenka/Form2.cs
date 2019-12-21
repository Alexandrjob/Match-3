using System;
using System.Drawing;
using System.Threading;
using System.Timers;
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
        int c = 0;
        bool a;
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

        Random r = new Random();//Это чтобы фигруки были всегда разные

        System.Timers.Timer Timer;
        MenuForm MenuForm = new MenuForm();//Ссылачка на меню
        ResultForm ResultForm = new ResultForm();
        Draw draw = new Draw();

        int gamepoints = 0;
        int gametime = 0;
        public Playspace()
        {
            InitializeComponent();
            this.Width = _width + 170;
            this.Height = _height + 35;
            _MapGenerate();
            _ArrayGenerate();

            Timer = new System.Timers.Timer();

            Timer.AutoReset = true; // Чтобы операции удаления перекрывались
            Timer.Interval = 1000;
            Timer.Elapsed += TimerTick;
            Timer.Enabled = true;
            Timer.Start();
        }
        private void _MapGenerate()
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

        private void _ArrayGenerate()
        {
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    NumberArrfigures[i, j] = r.Next(1, 6);
                    _FruitGenerate(j, i, NumberArrfigures[i, j]);
                    //НЕ пойму в чем прикол до сих пор
                }
            }
        }

        private void NumberVisibl()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write("{0}\t", NumberArrfigures[i, j]);
                }
                Console.WriteLine("\n");
            }
        }

        private void SameVisibl()
        {
            foreach (int add in sameElements)
            {
                if (c != 7)
                {
                    c++;
                    Console.Write("{0} ", add);
                }
                else
                {
                    Console.Write("{0} ", add);
                    Console.WriteLine("\t");
                    c = 0;
                }
            }
        }

        private void _FruitGenerate(int i, int j, int value)

        {
            //Если поменять местами i и j то баг исправится
            switch (value)
            {
                case 1:
                    Draw.X(i, j);
                    break;
                case 2:
                    Draw.Circle(i, j);
                    break;
                case 3:
                    Draw.Rectangle(i, j);
                    break;
                case 4:
                    Draw.Triangle(i, j);
                    break;
                case 5:
                    Draw.TriangleReverse(i, j);
                    break;
            }
        }

        
        void moving()
        {
            int counter = 0;
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
                            //Если ячейка 0-го ряда пуста, то генерируется элемент и опускается вниз
                            if (i - counter - h < 0)
                            {
                                for (int u = 0; u < counter; u++)
                                {
                                    //В случае, когда опускать нечего, мы просто создаем элемент и останавливаем цикл
                                    if (counter - 1 - u == 0)
                                    {
                                        CreatNewPictureBox(j);
                                        break;
                                    }
                                    CreatNewPictureBox(j);
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
            NumberArrfigures[0, y] = r.Next(1, 6);
            _FruitGenerate(y, 0, NumberArrfigures[0, y]);
        }

        //Проверка на одинаковые элеметы
        private void CheckingForIdenticalElements()
        {
            a = false;
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
                gamepoints += 100;
                labelpoints.Text = gamepoints.ToString();
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
            a = true;
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
                while (a);
                NumberVisibl();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Timer.Stop();
            Timer.Dispose();
            this.Close();
            this.Dispose();
            MenuForm.Visible = true;
        }



        public delegate void InvokeDelegate();

        private void TimerTick(Object sourse, ElapsedEventArgs e)
        {
            //Через такой подходтоже не работает(сначало завершается основной поток потом только время)
            labelTime.BeginInvoke(new InvokeDelegate(InvokeMethod));

            //this.labelTime.Text = (60 - gametime).ToString();
            //this.labelTime.Refresh();

            if (60 - gametime == 58)
            {
                Timer.Stop();
                Timer.Dispose();
                //Как заблокировать или остановить форму
                //this.Enabled = false;

                ResultForm.Show();
                ResultForm.Enabled = true;
                //Создать на ней кол-во набранных очков
                //кнопку в меню, если нажали то закрыть форму игры и окно результатов
                //Кнопку заново, если нажали то переаапустить форму игры и закрыть окго результатов
            }
            else gametime++;
        }

        public void InvokeMethod()
        {
            labelTime.Text = (60 - gametime).ToString();
        }
    }
}
