using System;

namespace Wavy
{
    class Program
    {
        private static Cell[,] grid = new Cell[8, 50];


        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Init();

            while (true)
            {
                Update();
                System.Threading.Thread.Sleep(50);
            }
        }

        private static void Init()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new Cell();
                    if (i == 0)
                    {
                        grid[i, j].Value = true;
                        SetBlack(i, j);
                    }
                    else
                    {
                        grid[i, j].Value = false;
                        SetBlack(i, j);
                    }
                }
            }
        }

        private static void Update()
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    if (grid[i, j].Value)
                    {
                        grid[i, j].Value = false;
                        SetBlack(i, j);

                        if (grid[0, j].IsDown)
                        {
                            int nextRow;
                            if (i == grid.GetLength(0) - 1)
                            {
                                grid[0, j].IsDown = false;
                                nextRow = i - 1;
                            }
                            else
                            {
                                nextRow = i + 1;
                            }

                            grid[nextRow, j].Value = true;
                            SetWhite(nextRow, j);
                        }
                        else
                        {
                            int nextRow;
                            if (i == 0)
                            {
                                grid[0, j].IsDown = true;
                                nextRow = i + 1;
                            }
                            else
                            {
                                nextRow = i - 1;
                            }

                            grid[nextRow, j].Value = true;
                            SetWhite(nextRow, j);
                        }

                        break;
                    }
                }

                if (!grid[0, j].IsStart)
                {
                    grid[0, j].IsStart = true;
                    break;
                }
            }
        }

        private static void SetWhite(int i, int j)
        {
            string fill = "  ";
            Console.SetCursorPosition(j * fill.Length, i);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(fill);
        }

        private static void SetBlack(int i, int j)
        {
            string fill = "  ";
            Console.SetCursorPosition(j * fill.Length, i);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(fill);
        }
    }

    public class Cell
    {
        public bool Value { get; set; } = false;
        public bool IsDown { get; set; } = true;
        public bool IsStart { get; set; } = false;
    }
}
