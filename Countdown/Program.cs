using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Countdown
{
    class Program
    {
        private static Cell[,] grid = new Cell[11, 41];

        static void Main(string[] args)
        {
            Countdown();
        }

        private static void Countdown()
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = true;
                bool isQuit = false;
                bool isCounting = false;
                int countdownSec = 0;
                while (true)
                {
                    Console.Write("Input Countdown Seconds: ");
                    string countdownSecStr = Console.ReadLine();
                    if (int.TryParse(countdownSecStr, out countdownSec))
                    {
                        if (countdownSec > 86400)
                        {
                            continue;
                        }

                        break;
                    }
                }

                Init();
                Console.CursorVisible = false;
                isCounting = true;

                Thread dotThread = new Thread(() =>
                {
                    ShowAllDot();

                    Stopwatch dotWatch = null;
                    while (true)
                    {
                        if (isQuit)
                        {
                            break;
                        }

                        if (dotWatch == null && isCounting)
                        {
                            dotWatch = Stopwatch.StartNew();
                        }

                        if (isCounting && dotWatch.ElapsedMilliseconds >= 500)
                        {
                            SwitchDot(DotLocation.First);
                            SwitchDot(DotLocation.Second);

                            dotWatch.Stop();
                            dotWatch = null;
                        }
                    }
                });
                dotThread.Start();

                Thread quitThread = new Thread(() =>
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Q)
                    {
                        isQuit = true;
                    }
                });
                quitThread.Start();

                Stopwatch stopwatch = null;
                while (true)
                {
                    if (isQuit)
                    {
                        break;
                    }

                    if (stopwatch == null && isCounting)
                    {
                        stopwatch = Stopwatch.StartNew();
                    }

                    // 时间到
                    if (stopwatch != null && stopwatch.Elapsed.TotalSeconds > countdownSec)
                    {
                        LeftTime(countdownSec, countdownSec);
                        ShowAllDot();
                        stopwatch.Stop();
                        stopwatch = null;
                        isCounting = false;

                    }
                    else if (isCounting)
                    {
                        LeftTime(countdownSec, (int)stopwatch.Elapsed.TotalSeconds);
                    }

                    Update();
                    Console.WriteLine("\n  Press q to quit");
                }
            }
        }

        private static void LeftTime(int countdownSec, int elapsedSec)
        {
            var timeLeft = TimeSpan.FromSeconds(countdownSec - elapsedSec);
            DisplayNumber(timeLeft.Hours / 10, NumberLocation.HourFirst);
            DisplayNumber(timeLeft.Hours % 10, NumberLocation.HourSecond);
            DisplayNumber(timeLeft.Minutes / 10, NumberLocation.MinuteFirst);
            DisplayNumber(timeLeft.Minutes % 10, NumberLocation.MinuteSecond);
            DisplayNumber(timeLeft.Seconds / 10, NumberLocation.SecondFirst);
            DisplayNumber(timeLeft.Seconds % 10, NumberLocation.SecondSecond);
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

                        grid[i, j].Value = false;
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

        private static void SwitchDot(DotLocation location)
        {
            int colOffset = (int)location;
            if (grid[3, colOffset].Value)
            {
                grid[3, colOffset].Value = false;
                SetBlack(3, colOffset);
            }
            else
            {
                grid[3, colOffset].Value = true;
                SetWhite(3, colOffset);
            }

            if (grid[7, colOffset].Value)
            {
                grid[7, colOffset].Value = false;
                SetBlack(7, colOffset);
            }
            else
            {
                grid[7, colOffset].Value = true;
                SetWhite(7, colOffset);
            }
        }

        private static void ShowAllDot()
        {
            int firstCol = (int)DotLocation.First;
            int secondCol = (int)DotLocation.Second;

            grid[3, firstCol].Value = true;
            SetWhite(3, firstCol);
            grid[7, firstCol].Value = true;
            SetWhite(7, firstCol);

            grid[3, secondCol].Value = true;
            SetWhite(3, secondCol);
            grid[7, secondCol].Value = true;
            SetWhite(7, secondCol);
        }

        private static void HideAllDot()
        {
            int firstCol = (int)DotLocation.First;
            int secondCol = (int)DotLocation.Second;

            grid[3, firstCol].Value = false;
            SetBlack(3, firstCol);
            grid[7, firstCol].Value = false;
            SetBlack(7, firstCol);

            grid[3, secondCol].Value = false;
            SetBlack(3, secondCol);
            grid[7, secondCol].Value = false;
            SetBlack(7, secondCol);
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

    public enum DotLocation
    {
        First = 13, Second = 27
    }
}
