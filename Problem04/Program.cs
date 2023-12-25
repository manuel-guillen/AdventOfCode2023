// Part 1
// ========================
static int CountWinningNumbers(string str) =>
    str[(str.IndexOf(':') + 2)..].Split(" | ")
    .Select(list => list.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToHashSet())
    .Aggregate((s1, s2) => s1.Intersect(s2).ToHashSet())
    .Count;

var winningNumbers = File.ReadLines("input.txt").Select(CountWinningNumbers);
int result = winningNumbers.Select(n => (int)Math.Pow(2, n - 1)).Sum();
Console.WriteLine(result);

// Part 2
// ========================
static (int, List<int>) CountTotalScratchcards((int sum, List<int> extraCards) acc, int wins)
{
    (int cards, List<int> extraCards) = acc.extraCards.Count > 0 ? (1 + acc.extraCards[0], acc.extraCards[1..]) : (1, acc.extraCards);
    for (int i = 0; i < wins; i++)
        try
        {
            extraCards[i] += cards;
        }
        catch (ArgumentOutOfRangeException)
        {
            extraCards.Add(cards);
        }
    return (acc.sum + cards, extraCards);
}

(int result2, _) = winningNumbers.Aggregate((0, new List<int>()), CountTotalScratchcards);
Console.WriteLine(result2);