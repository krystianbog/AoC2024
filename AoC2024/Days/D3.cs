using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC2024.Days;

internal static partial class D3
{
    internal static void Execute()
    {
        string inputFilePath = Path.Combine(AppContext.BaseDirectory, @"Inputs\InputD3.txt");

        string input = File.ReadAllText(inputFilePath);

        List<string> partOneMulOperations = GetOperationsBasedOnRegex(input, ValidMulOperationPattern());

        Console.WriteLine($"Day 3 Part 1: {CalculateMulOperations(partOneMulOperations)}");

        List<string> partTwoAllOperations = GetOperationsBasedOnRegex(input, ValidMulOperationAndDoInstructionsPattern());

        List<string> partTwoMulOperations = GetValidMulOperations(partTwoAllOperations);

        Console.WriteLine($"Day 3 Part 2: {CalculateMulOperations(partTwoMulOperations)}");
    }

    private static List<string> GetOperationsBasedOnRegex(string input, Regex regex) =>
        regex.Matches(input).Select(x => x.Value).ToList();

    private static List<string> GetValidMulOperations(List<string> operations)
    {
        List<string> mulOperations = new();

        bool acceptMulOperation = true;

        for (int i = 0; i < operations.Count; i++)
        {
            if (DontOperationPattern().Match(operations[i]).Success)
            {
                acceptMulOperation = false;
            }

            if (DoOperationPattern().Match(operations[i]).Success)
            {
                acceptMulOperation = true;
            }

            if (ValidMulOperationPattern().Match(operations[i]).Success && acceptMulOperation)
            {
                mulOperations.Add(operations[i]);
            }
        }

        return mulOperations;
    }

    private static BigInteger CalculateMulOperations(List<string> mulOperations)
    {
        BigInteger sum = 0;

        foreach (var mul in mulOperations)
        {
            var mulParameters =
                NumberUpTo3DigitsPattern()
                .Matches(mul)
                .Select(x => int.Parse(x.Value))
                .ToList();

            sum += mulParameters[0] * mulParameters[1];
        }

        return sum;
    }

    [GeneratedRegex(@"mul\(\d{1,3}\,\d{1,3}\)")]
    private static partial Regex ValidMulOperationPattern();

    [GeneratedRegex(@"\d{1,3}")]
    private static partial Regex NumberUpTo3DigitsPattern();

    [GeneratedRegex(@"mul\(\d{1,3}\,\d{1,3}\)|do\(\)|don\'t\(\)")]
    private static partial Regex ValidMulOperationAndDoInstructionsPattern();

    [GeneratedRegex(@"don\'t\(\)")]
    private static partial Regex DontOperationPattern();

    [GeneratedRegex(@"do\(\)")]
    private static partial Regex DoOperationPattern();
}
