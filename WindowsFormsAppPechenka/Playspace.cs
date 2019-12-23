using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timers = System.Timers;

namespace WindowsFormsAppPechenka
{
    public partial class PlayForm : Form
    {
        //Для размеров формы и _sizeOfSides для размера ячеек и фигурок
        private readonly int _width = 320;
        private readonly int _height = 320;
        private readonly int _sizeOfSides = 40;
        //*****

        //Любимые массивы(В них хранятся фигурки и их номера)
        public static PictureBox[,] PictureArrfigures = new PictureBox[8, 8];
        public static int[,] NumberArrfigures = new int[8, 8];

        //*****
        readonly Random random = new Random();//Это чтобы фигруки были всегда разные

        private int _gameSecondsLeft = 60;
        private const int GameSecondsLeftWhenResultStart = 0;

        private readonly Timers.Timer _timer;
        private readonly Form _mainForm;
        public static readonly Thread Thread;
        private readonly Elements elements;
        //readonly Elements elements = new Elements();

        public PlayForm(Form mainForm)
        {
            _mainForm = mainForm;
            elements = new Elements(this);
            InitializeComponent();
            this.Width = _width + 170;
            this.Height = _height + 35;
            MapGenerate();
            ArraysGenerate();

            _timer = new Timers.Timer
            {
                AutoReset = true,
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

        public void moving()
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
                                    NumberArrfigures[i - h - u, j] = NumberArrfigures[0, j];
                                    NumberArrfigures[0, j] = 0;

                                    PictureBox picturebox = PictureArrfigures[j, 0];
                                    PictureArrfigures[j, 0] = null;
                                    picturebox.Location = new Point(j * 40, (i - h - u) * 40);
                                    Thread.Sleep(60);
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
                                Thread.Sleep(60);
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

        private void PictureBox_Click(object sender, EventArgs e)
        {
           elements.bla(sender);
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
            ResultForm ResultForm = new ResultForm(_mainForm, this, InteractionWitchFigure.gamepoint);
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
