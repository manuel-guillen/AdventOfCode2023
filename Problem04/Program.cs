// Part 1
// ========================
static int CountWinningNumbers(string str) =>
    str[(str.IndexOf(':') + 2)..].Split(" | ")
    .Select(list => list.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToHashSet())
    .Aggregate((s1, s2) => s1.Intersect(s2).ToHashSet())
    .Count;

int result = File.ReadLines("input.txt").Select(CountWinningNumbers).Select(n => (int)Math.Pow(2, n-1)).Sum();
Console.WriteLine(result);