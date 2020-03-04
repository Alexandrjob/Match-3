using System;
using System.Drawing;
using System.Threading;

namespace WindowsFormsAppPechenka
{
    class InteractionWitchFigure
    {
        public static int gamepoint;
        public bool isfiguresdelete;

        private int[,] sameElements = new int[4, 8];//массив для номерков фигурок для их удаления
        private int countergorizontal = 0;
        private int countervertical = 0;

        private readonly FigureDataBase FigureDataBase;

        public InteractionWitchFigure(FigureDataBase figureDataBase)
        {
            FigureDataBase = figureDataBase;
        }
        public void CheckingForIdenticalElements()
        {
            isfiguresdelete = false;
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
                    DeleteElement(countergorizontal, 0);
                }
                if (countervertical >= 2)
                {
                    DeleteElement(countervertical, 2);
                }
                Array.Clear(sameElements, 0, 32);
                countervertical = 0;
                countergorizontal = 0;
            }
        }

        private void CheckGorizontal(int i, int j)
        {
            if (FigureDataBase.NumberArrfigures[i, j] == FigureDataBase.NumberArrfigures[i, j + 1] & FigureDataBase.NumberArrfigures[i, j] != 0)
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
                DeleteElement(countergorizontal, 0);
                Array.Clear(sameElements, 0, 16);
                countergorizontal = 0;
            }
            else
            {
                Array.Clear(sameElements, 0, 16);
                countergorizontal = 0;
            }
        }

        private void CheckVertical(int i, int j)
        {
            if (FigureDataBase.NumberArrfigures[j, i] == FigureDataBase.NumberArrfigures[j + 1, i] & FigureDataBase.NumberArrfigures[j, i] != 0)
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
                DeleteElement(countervertical, 2);
                Array.Clear(sameElements, 16, 16);
                countervertical = 0;
            }
            else
            {
                Array.Clear(sameElements, 16, 16);
                countervertical = 0;
            }
        }

        private void DeleteElement(int counter, int deletepoint)
        {
            for (int p = 0; p < counter + 1; p++)
            {
                gamepoint += 100;
                PlayForm.labelPoints.Text = gamepoint.ToString();
                PlayForm.labelPoints.Refresh();
    
                int i = sameElements[deletepoint, p];
                int j = sameElements[deletepoint + 1, p];
                FigureDataBase.NumberArrfigures[i, j] = 0;
                if (FigureDataBase.PictureArrfigures[j, i] != null)
                {
                    FigureDataBase.PictureArrfigures[j, i].BackColor = Color.Aqua;
                    FigureDataBase.PictureArrfigures[j, i].Refresh();
                    Thread.Sleep(50);
                    FigureDataBase.PictureArrfigures[j, i].Dispose();
                    FigureDataBase.PictureArrfigures[j, i] = null;
                }
            }
            isfiguresdelete = true;
        }

    }
}
