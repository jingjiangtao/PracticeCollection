# CellularAutomata
**If there is an exception, please modify the size of the console.**

```
System.ArgumentOutOfRangeException: The new console window size would force the console buffer size to be too large. (Parameter 'height')
Actual value was 120.
   at System.ConsolePal.SetWindowSize(Int32 width, Int32 height)
   at System.Console.set_WindowHeight(Int32 value)
   at CellularAutomata.Program.Init() in D:\mydata\source\repos\CellularAutomata\CellularAutomata\Program.cs:line 38
   at CellularAutomata.Program.Main(String[] args) in D:\mydata\source\repos\CellularAutomata\CellularAutomata\Program.cs:line 17
```

```
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
```
