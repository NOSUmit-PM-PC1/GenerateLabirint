using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateLabirint
{
    class Program
    {
        static Random rnd = new Random();
        static char[,] GenerateLabirint(int w, int h)
        {
            char[,] field = new char[h, w];
            char[] symbols = new char[] { 'x', '0' };

            for (int i = 0; i < h; i++)
            {
                field[i, 0] = 'x';
                field[i, w - 1] = 'x';
            }
            for (int i = 0; i < w; i++)
            {
                field[0, i] = 'x';
                field[h - 1, i] = 'x';
            }

            for (int i = 1; i < h - 1; i++)
            {
                for (int j = 1; j < w - 1; j++)
                {
                    if (rnd.Next(0, 3) == 1)
                        field[i, j] = symbols[0];
                    else
                        field[i, j] = symbols[1];
                }
            }

            // установить точку старта
            field[1, 1] = '0';
            // установить выход из лабиринта
            field[1, w - 1] = '0';

            //PrintMass2(field);
            return field;
        }

        static bool CheckRightLabint(char[,] field)
        {
            int h = field.GetLength(0);
            int w = field.GetLength(1);
            int[,] check = new int[h, w];
            
            int step = 1;
            check[1, 1] = step;
            bool flagChange = true;
            while (check[1, w - 1] == 0 && flagChange)
            {
                flagChange = false;
                //for(int step = 1; step < 20; step++)
                for (int i = 1; i < h - 1; i++)
                {
                    for (int j = 1; j < w - 1; j++)
                    {
                        if (check[i, j] == step)
                        {
                            if (field[i - 1, j] == '0' && check[i - 1, j] == 0) 
                                { check[i - 1, j] = step + 1; flagChange = true;}
                            if (field[i + 1, j] == '0' && check[i + 1, j] == 0)
                                { check[i + 1, j] = step + 1; flagChange = true;}
                            if (field[i, j - 1] == '0' && check[i, j - 1] == 0)
                                { check[i, j - 1] = step + 1; flagChange = true; }
                                if (field[i, j + 1] == '0' && check[i, j + 1] == 0)
                                { check[i, j + 1] = step + 1; flagChange = true; }
                        }
                    }
                }
                step++;
            }
            Console.WriteLine(flagChange);
            //PrintMass2(check);
            //Console.ReadKey();
            return flagChange;
        }

        static void PrintMass2(char[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write(string.Format(" {0:00}", m[i, j]));
                }
                Console.WriteLine();
            }
        }

        static void PrintMass2(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write(string.Format(" {0:00}", m[i, j]));
                }
                Console.WriteLine();
            }
        }

        static void SaveLabirint(string fileName, int w, int h)
        {
            char[,] lab;
            do
            {
                lab = GenerateLabirint(w, h); 
            }
            while (!CheckRightLabint(lab));

            StreamWriter wr = new StreamWriter(fileName);
            wr.WriteLine($"{w} {h}");
            //wr.WriteLine(w + " " + h);
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    wr.Write(lab[i, j]);
                }
                wr.WriteLine();
            }
            wr.Close();
        }


        static void Main(string[] args)
        {
            SaveLabirint("level2.txt", 20, 20);

        }
    }
}
