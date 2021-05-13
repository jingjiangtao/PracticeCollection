using System;

namespace CellularAutomata
{
    class Program
    {
        private static int gridRowCol = 32;
        private static Cell[,] grid = new Cell[gridRowCol, gridRowCol];
        private static int sleepMs = 33;
        private static int initAlivePossibility = 4; // 4 means 1/4

        static void Main(string[] args)
        {
            try
            {
                Console.CursorVisible = false;

                Init();
                // Main loop
                while (true)
                {
                    Update();
                    System.Threading.Thread.Sleep(sleepMs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }

        private static void Init()
        {
            // Set Console Size
            Console.BufferHeight = 256;
            Console.BufferWidth = 256;
            Console.WindowWidth = 256;
            Console.WindowHeight = 80;

            Random random = new Random();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new Cell();
                    int value = random.Next(0, initAlivePossibility);
                    if (value == 0)
                    {
                        grid[i, j].Value = true;
                    }
                    else
                    {
                        grid[i, j].Value = false;
                    }
                }
            }
        }

        private static void Update()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    int aliveCount = NeighborAliveCount(i, j);

                    if (grid[i, j].Value) // Alive
                    {
                        if (aliveCount < 2 || aliveCount > 3)
                        {
                            grid[i, j].Value = false;
                        }
                    }
                    else // Death
                    {
                        if (aliveCount == 3)
                        {
                            grid[i, j].Value = true;
                        }
                    }

                    if (grid[i, j].Value)
                    {
                        SetAlive(i, j);
                    }
                    else
                    {

                        SetDeath(i, j);
                    }
                }
            }
        }

        private static int NeighborAliveCount(int i, int j)
        {
            int count = 0;
            for (int m = i - 1; m <= i + 1; m++)
            {
                for (int n = j - 1; n <= j + 1; n++)
                {
                    if (m == i && n == j) continue;
                    if (m < 0 || m >= grid.GetLength(0)) continue;
                    if (n < 0 || n >= grid.GetLength(1)) continue;
                    if (grid[m, n].Value) count++;
                }
            }

            return count;
        }

        private static void SetAlive(int i, int j)
        {
            string aliveStr = "  ";
            Console.SetCursorPosition(j * aliveStr.Length, i);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(aliveStr);
        }

        private static void SetDeath(int i, int j)
        {
            string deathStr = "  ";
            Console.SetCursorPosition(j * deathStr.Length, i);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(deathStr);
        }
    }

    public class Cell
    {
        public bool Value { get; set; }
    }
}
