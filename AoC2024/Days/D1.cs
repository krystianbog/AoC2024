namespace AoC2024.Days;

internal static class D1
{
    internal static void Execute()
    {
        Console.WriteLine("D1 init");

        string inputFilePath = Path.Combine(AppContext.BaseDirectory, @"Inputs\InputD1.txt");

        string input = File.ReadAllText(inputFilePath);

        List<string> inputList = input
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        List<int> leftList = new();
        List<int> rightList = new();

        foreach (var inputLine in inputList)
        {
            var inputValues = inputLine.Split("   ");

            leftList.Add(int.Parse(inputValues[0]));
            rightList.Add(int.Parse(inputValues[1]));
        }

        leftList.Sort();
        rightList.Sort();

        int absoluteDifferenceSum = 0;

        for (int i = 0; i < leftList.Count; i++)
        {
            absoluteDifferenceSum += Math.Abs(leftList[i] - rightList[i]);
        }

        Console.WriteLine($"D1 PT1: {absoluteDifferenceSum}");

        int multiplierSum = 0;

        foreach (var value in leftList)
        {
            multiplierSum += value * rightList.Count(x => x == value);
        }

        Console.WriteLine($"D1 PT2: {multiplierSum}");
    }
}
