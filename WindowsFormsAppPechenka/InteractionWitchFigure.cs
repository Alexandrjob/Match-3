using System;
using System.Drawing;
using System.Threading;

namespace WindowsFormsAppPechenka
{
    static class InteractionWitchFigure
    {
        public static int gamepoint;
        public static bool isfiguresdelete;

        private static int[,] sameElements = new int[4, 8];//массив для номерков фигурок для их удаления
        private static int countergorizontal = 0;
        private static int countervertical = 0;

        public static void CheckingForIdenticalElements()
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

        private static void CheckGorizontal(int i, int j)
        {
            if (PlayForm.NumberArrfigures[i, j] == PlayForm.NumberArrfigures[i, j + 1] & PlayForm.NumberArrfigures[i, j] != 0)
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

        private static void CheckVertical(int i, int j)
        {
            if (PlayForm.NumberArrfigures[j, i] == PlayForm.NumberArrfigures[j + 1, i] & PlayForm.NumberArrfigures[j, i] != 0)
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

        private static void DeleteElement(int counter, int deletepoint)
        {
            for (int p = 0; p < counter + 1; p++)
            {
                gamepoint += 100;
                PlayForm.labelgamepoint.Text = gamepoint.ToString();
                PlayForm.labelgamepoint.Refresh();

                int i = sameElements[deletepoint, p];
                int j = sameElements[deletepoint + 1, p];
                PlayForm.NumberArrfigures[i, j] = 0;
                if (PlayForm.PictureArrfigures[j, i] != null)
                {
                    PlayForm.PictureArrfigures[j, i].BackColor = Color.Aqua;
                    PlayForm.PictureArrfigures[j, i].Refresh();
                    Thread.Sleep(50);
                    PlayForm.PictureArrfigures[j, i].Dispose();
                    PlayForm.PictureArrfigures[j, i] = null;
                }
            }
            isfiguresdelete = true;
        }

    }
}
