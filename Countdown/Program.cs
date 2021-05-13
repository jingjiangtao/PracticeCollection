using System;
using System.Diagnostics;

namespace Countdown
{
    class Program
    {
        private static Cell[,] grid = new Cell[11, 41];

        static void Main(string[] args)
        {

            Init();
            
            while (true)
            {
                var stopwatch = Stopwatch.StartNew();
                CurrentTime();
                Update();
                stopwatch.Stop();

                System.Threading.Thread.Sleep(500 - (int)stopwatch.ElapsedMilliseconds);
            }
        }

        private static void CurrentTime()
        {
            var now = DateTime.Now;
            DisplayNumber(now.Hour / 10, NumberLocation.HourFirst);
            DisplayNumber(now.Hour % 10, NumberLocation.HourSecond);
            DisplayNumber(now.Minute / 10, NumberLocation.MinuteFirst);
            DisplayNumber(now.Minute % 10, NumberLocation.MinuteSecond);
            DisplayNumber(now.Second / 10, NumberLocation.SecondFirst);
            DisplayNumber(now.Second % 10, NumberLocation.SecondSecond);
        }


        private static void Init()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new Cell();
                    grid[i, j].Value = false;
                }
            }
        }

        private static void Update()
        {
            DisplayDot(13);
            DisplayDot(27);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j].Value)
                    {
                        SetWhite(i, j);
                    }
                    else
                    {
                        SetBlack(i, j);
                    }
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

        private static void DisplayNumber(int number, NumberLocation location)
        {
            int colOffset = (int)location;
            switch (number)
            {
                case 0:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1 || i == 9)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8)
                        {
                            if (j == colOffset || j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 1:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (j == colOffset + 4)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 2:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1 || i == 5 || i == 9)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4)
                        {
                            if (j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        if (i == 6 || i == 7 || i == 8)
                        {
                            if (j == colOffset)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 3:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1 || i == 5 || i == 9)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4 || i == 6 || i == 7 || i == 8)
                        {
                            if (j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 4:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 5)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 1 || i == 2 || i == 3 || i == 4)
                        {
                            if (j == colOffset || j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        if (i == 6 || i == 7 || i == 8 || i == 9)
                        {
                            if (j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 5:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1 || i == 5 || i == 9)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4)
                        {
                            if (j == colOffset)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        if (i == 6 || i == 7 || i == 8)
                        {
                            if (j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 6:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1 || i == 5 || i == 9)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4)
                        {
                            if (j == colOffset)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        if (i == 6 || i == 7 || i == 8)
                        {
                            if (j == colOffset || j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 7:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9)
                        {
                            if (j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
                case 8:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1 || i == 5 || i == 9)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4 || i == 6 || i == 7 || i == 8)
                        {
                            if (j == colOffset || j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }
                    });
                    break;
                case 9:
                    TraverseNumber(colOffset, (i, j) =>
                    {
                        if (i == 1 || i == 5 || i == 9)
                        {
                            grid[i, j].Value = true;
                            return;
                        }

                        if (i == 2 || i == 3 || i == 4)
                        {
                            if (j == colOffset || j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        if (i == 6 || i == 7 || i == 8)
                        {
                            if (j == colOffset + 4)
                            {
                                grid[i, j].Value = true;
                                return;
                            }
                        }

                        grid[i, j].Value = false;
                    });
                    break;
            }
        }

        private static void TraverseNumber(int colOffset, Action<int, int> action)
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = colOffset; j < colOffset + 5; j++)
                {
                    action(i, j);
                }
            }
        }

        private static void DisplayDot(int colOffset)
        {
            if (grid[3, colOffset].Value)
            {
                grid[3, colOffset].Value = false;
            }
            else
            {
                grid[3, colOffset].Value = true;
            }

            if (grid[7, colOffset].Value)
            {
                grid[7, colOffset].Value = false;
            }
            else
            {
                grid[7, colOffset].Value = true;
            }
        }
    }

    public class Cell
    {
        public bool Value { get; set; }
    }

    public enum NumberLocation
    {
        HourFirst = 1, HourSecond = 7,
        MinuteFirst = 15, MinuteSecond = 21,
        SecondFirst = 29, SecondSecond = 35,
    }
}
