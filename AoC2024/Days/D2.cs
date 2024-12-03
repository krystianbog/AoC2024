namespace AoC2024.Days;

internal static class D2
{
    internal static void Execute()
    {
        Console.WriteLine("D2 init");

        string inputFilePath = Path.Combine(AppContext.BaseDirectory, @"Inputs\InputD2.txt");

        string input = File.ReadAllText(inputFilePath);

        var reportList = input
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x =>
                x.Split(" ")
                .Select(int.Parse)
                .ToList())
            .ToList();

        int resultPart1 = reportList.Count(IsSafeReport);

        Console.WriteLine($"D2 PT1: {resultPart1}");

        int resultPart2 = reportList.Count(IsSafeReportWithDampener);

        Console.WriteLine($"D2 PT2: {resultPart2}");
    }

    private static bool IsSafeReport(List<int> reportValues)
    {
        for (int i = 1; i < reportValues.Count; i++)
        {
            if (Math.Abs(reportValues[i - 1] - reportValues[i]) is 0 or > 3)
            {
                return false;
            }
        }

        return reportValues.SequenceEqual(reportValues.OrderBy(x => x))
            || reportValues.SequenceEqual(reportValues.OrderByDescending(x => x));
    }

    private static bool IsSafeReportWithDampener(List<int> reportValues) =>
        reportValues.Where((_, singleValue) => IsSafeReport(reportValues.Where((_, j) => j != singleValue)
            .ToList()))
        .Any();

    #region pt1 meh

    internal enum DifferenceType
    {
        NotNoted = 0,
        Increasing = 1,
        Decresing = 2,
    }

    internal static void ExecuteMeh()
    {
        Console.WriteLine("D2 init");

        string inputFilePath = Path.Combine(AppContext.BaseDirectory, @"Inputs\InputD2.txt");

        string input = File.ReadAllText(inputFilePath);

        List<string> reportList = input
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        int safeReportsCounter = 0;

        foreach (var report in reportList)
        {
            var reportValues = report
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            bool reportSafe = true;

            DifferenceType currentDifference = DifferenceType.NotNoted;
            DifferenceType previousDifference = DifferenceType.NotNoted;

            for (int i = 1; i < reportValues.Count; i++)
            {
                var currentValue = reportValues[i];
                var previousValue = reportValues[i - 1];

                if (currentValue == previousValue)
                {
                    reportSafe = false;

                    break;
                }

                var absDiff = Math.Abs(currentValue - previousValue);

                if (absDiff > 3)
                {
                    reportSafe = false;

                    break;
                }

                currentDifference = currentValue > previousValue ? DifferenceType.Increasing : DifferenceType.Decresing;

                if (i == 1)
                {
                    previousDifference = currentDifference;

                    continue;
                }

                if (i > 1 && currentDifference != previousDifference)
                {
                    reportSafe = false;

                    break;
                }

                previousDifference = currentDifference;
            }

            if (reportSafe)
            {
                safeReportsCounter++;
            }
        }
    }
    #endregion
}
