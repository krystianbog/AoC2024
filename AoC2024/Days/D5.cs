using AoC2024.Core;
using System.Data;

namespace AoC2024.Days;

internal class D5
{
    //Access
    List<List<int>> pageOrderingRules = [];
    List<List<int>> pageUpdatesNumbers = [];

    internal void Execute()
    {
        string inputFilePath = Path.Combine(AppContext.BaseDirectory, @"Inputs\D5.txt");

        string input = File.ReadAllText(inputFilePath);

        var inputParts = input.Split("\n\n");

        pageOrderingRules = inputParts[0]
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList()
            .ConvertAll(x => x.Split("|").Select(int.Parse)
            .ToList())
;
        pageUpdatesNumbers = inputParts[1]
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList()
            .ConvertAll(x => x.Split(",").Select(int.Parse)
            .ToList());

        PerformPuzzle(PuzzlePart.One);
        PerformPuzzle(PuzzlePart.Two);
    }

    private void PerformPuzzle(PuzzlePart part)
    {
        int result = 0;

        foreach (var pagesUpdateNumbers in pageUpdatesNumbers)
        {
            List<List<int>> applicableRules =
                pageOrderingRules
                .Where(x => x.All(pagesUpdateNumbers.Contains))
                .ToList();

            if (part == PuzzlePart.One)
            {
                if (applicableRules.All(x => RespectsRule(pagesUpdateNumbers, x)))
                {
                    result += pagesUpdateNumbers[pagesUpdateNumbers.Count / 2];
                }
            }

            if (part == PuzzlePart.Two)
            {
                if (!applicableRules.All(x => RespectsRule(pagesUpdateNumbers, x)))
                {
                    CorrectRules(pagesUpdateNumbers, applicableRules);

                    result += pagesUpdateNumbers[pagesUpdateNumbers.Count / 2];
                }
            }
        }

        Console.WriteLine($"Day 5 Part {part}: {result}");
    }

    private static bool RespectsRule(List<int> pageUpdateNumbers, List<int> applicableRules)
    {
        if (applicableRules.Count < 1)
        {
            return true;
        }

        int lastIndex = pageUpdateNumbers.IndexOf(applicableRules[0]);

        if (lastIndex < 0)
        {
            return true;
        }

        for (int i = 1; i < applicableRules.Count; ++i)
        {
            int index = pageUpdateNumbers.IndexOf(applicableRules[i]);

            if (index < 0)
            {
                return true;
            }

            if (index < lastIndex)
            {
                return false;
            }

            lastIndex = index;
        }

        return true;
    }

    private static int CorrectRule(List<int> pageUpdateNumbers, List<int> applicableRules)
    {
        if (applicableRules.Count < 1)
        {
            return 0;
        }

        bool hasCorrected = true;

        int corrected = 0;

        while (hasCorrected)
        {
            hasCorrected = false;

            int lastIndex = pageUpdateNumbers.IndexOf(applicableRules[0]);

            if (lastIndex < 0)
            {
                return 0;
            }

            for (int i = 0; i < applicableRules.Count; i++)
            {
                int index = pageUpdateNumbers.IndexOf(applicableRules[i]);

                if (index < 0)
                {
                    return 0;
                }

                if (index < lastIndex)
                {
                    (pageUpdateNumbers[index], pageUpdateNumbers[lastIndex])
                        = (pageUpdateNumbers[lastIndex], pageUpdateNumbers[index]);

                    hasCorrected = true;
                    corrected++;

                    break;
                }

                lastIndex = index;
            }
        }

        return corrected;
    }

    private static int CorrectRules(List<int> pageUpdateNumbers, List<List<int>> applicableRules)
    {
        bool hasCorrected = true;

        int correctedCounter = 0;

        while (hasCorrected)
        {
            hasCorrected = false;

            foreach (List<int> applicableRule in applicableRules)
            {
                int temporary = CorrectRule(pageUpdateNumbers, applicableRule);

                if (temporary > 0)
                {
                    correctedCounter += temporary;
                    hasCorrected = true;

                    break;
                }
            }
        }

        return correctedCounter;
    }
}
