namespace AoC2024.Days;

internal class D4
{
    //Access
    private int rowCount = 0;
    private int colCount = 0;
    private List<string> inputList = new();
    private readonly char[] word = ['M', 'A', 'S'];

    internal void Execute()
    {
        string inputFilePath = Path.Combine(AppContext.BaseDirectory, @"Inputs\D4.txt");

        string input = File.ReadAllText(inputFilePath);

        inputList = input
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        colCount = inputList[0].Length;
        rowCount = inputList.Count;

        int partOneResult = 0;
        int partTwoResult = 0;

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                if (inputList[row][col] == 'X')
                {
                    partOneResult += Find(row, col);
                }
                else if (inputList[row][col] == 'A')
                {
                    partTwoResult += FindX(row, col);
                }
            }
        }

        Console.WriteLine($"Day 4 Part 1: {partOneResult}");

        Console.WriteLine($"Day 4 Part 2: {partTwoResult}");
    }

    private bool IsInvalidRow(int rowIndex)
        => rowIndex >= rowCount || rowIndex < 0;

    private bool IsInvalidCol(int colIndex)
        => colIndex >= colCount || colIndex < 0;

    private int FindInDirection(int row, int col, int dirRow, int dirCol)
    {
        for (int i = 0; i < word.Length; i++)
        {
            var indexRow = row + (i * dirRow) + dirRow;
            var indexCol = col + (i * dirCol) + dirCol;

            if (IsInvalidRow(indexRow) ||
                IsInvalidCol(indexCol) ||
                inputList[indexRow][indexCol] != word[i])
            {
                return 0;
            }
        }

        return 1;
    }

    private int Find(int row, int col)
    {
        int[] dirs = [-1, 0, 1];

        int result = 0;

        for (int dirRow = 0; dirRow < dirs.Length; dirRow++)
        {
            for (int dirCol = 0; dirCol < dirs.Length; dirCol++)
            {
                result += FindInDirection(row, col, dirs[dirRow], dirs[dirCol]);
            }
        }

        return result;
    }

    private bool FindXChar(int row, int col, int dirRow, int dirCol, char searchedChar)
    {
        int indexRow = row + dirRow;
        int indexCol = col + dirCol;

        return !(IsInvalidRow(indexRow) ||
                 IsInvalidCol(indexCol) ||
                 inputList[indexRow][indexCol] != searchedChar);
    }

    private bool FindXHalf(int row, int col, int dirRow, int dirCol)
    {
        return
            FindXChar(row, col, dirRow, dirCol, 'M') &&
            FindXChar(row, col, dirRow * -1, dirCol * -1, 'S');
    }

    private int FindX(int row, int col)
    {
        int[] dirs = [1, -1];

        int result = 0;

        for (int dirRow = 0; dirRow < dirs.Length; dirRow++)
        {
            for (int dirCol = 0; dirCol < dirs.Length; dirCol++)
            {
                result += FindXHalf(row, col, dirs[dirRow], dirs[dirCol]) ? 1 : 0;
            }
        }

        return result == 2 ? 1 : 0;
    }
}
