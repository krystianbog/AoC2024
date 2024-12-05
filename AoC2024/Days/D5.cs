namespace AoC2024.Days;

internal class D5
{
    internal void Execute()
    {
        string inputFilePath = Path.Combine(AppContext.BaseDirectory, @"Inputs\D5.txt");

        string example = @"
47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47
";

        string input = File.ReadAllText(inputFilePath);
        //string input = example;

        var inputParts = input.Split("\n\n");
        //var inputParts = input.Split("\r\n\r\n");

        var pagesOrderingRules = inputParts[0]
            //.Split("\r\n")
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        var pagesToUpdate = inputParts[1]
            //.Split("\r\n")
            .Split("\n")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        Dictionary<int, List<int>> beforePageOrderingRulesDictionary = new();
        Dictionary<int, List<int>> afterPageOrderingRulesDictionary = new();

        foreach (var rule in pagesOrderingRules)
        {
            var ruleValues = rule.Split("|").Select(int.Parse).ToList();

            var numberBefore = ruleValues[0];
            var numberAfter = ruleValues[1];

            if (beforePageOrderingRulesDictionary.TryGetValue(numberAfter, out List<int>? valuesBefore))
            {
                valuesBefore.Add(numberBefore);
            }
            else
            {
                beforePageOrderingRulesDictionary.Add(numberAfter, new() { numberBefore });
            }

            if (afterPageOrderingRulesDictionary.TryGetValue(numberBefore, out List<int>? valuesAfter))
            {
                valuesAfter.Add(numberAfter);
            }
            else
            {
                afterPageOrderingRulesDictionary.Add(numberBefore, new() { numberAfter });
            }
        }

        List<int[]> validPagesUpdates = new();

        bool rowValid = true;

        foreach (var pagesRow in pagesToUpdate)
        {
            var pagesRowArray = pagesRow.Split(",").Select(int.Parse).ToArray();

            int rangeToIndex = 1;
            foreach (var pageRow in pagesRowArray)
            {
                var pageRowArraySliceBeforeCurrentValue = pagesRowArray[0..rangeToIndex];
                var pageRowArraySliceAfterCurrentValue =
                    pagesRowArray[(rangeToIndex - 1)..pagesRowArray.Length];

                if (beforePageOrderingRulesDictionary.TryGetValue(pageRow, out List<int>? pageRulesBefore))
                {
                    foreach (var pageRule in pageRulesBefore)
                    {
                        if (pagesRowArray.Contains(pageRule)
                            && !pageRowArraySliceBeforeCurrentValue.Contains(pageRule))
                        {
                            rowValid = false;
                        }
                    }
                }

                if (afterPageOrderingRulesDictionary.TryGetValue(pageRow, out List<int>? pageRulesAfter))
                {
                    foreach (var pageRule in pageRulesAfter)
                    {
                        if (pagesRowArray.Contains(pageRule)
                            && !pageRowArraySliceAfterCurrentValue.Contains(pageRule))
                        {
                            rowValid = false;
                        }
                    }
                }

                rangeToIndex++;
            }

            if (rowValid)
            {
                validPagesUpdates.Add(pagesRowArray);
            }
        }

        var partOneResult = 0;

        foreach (var validRow in validPagesUpdates)
        {
            partOneResult += validRow[validRow.Length / 2];
        }

        var partTwoResult = 0;

        Console.WriteLine($"Day 5 Part 1: {partOneResult}");

        Console.WriteLine($"Day 5 Part 2: {partTwoResult}");
    }
}
