using System.Text.RegularExpressions;

// Part 1
// ========================
HashSet<(int,int)> partPositions = 
    File.ReadLines("input.txt")
    .SelectMany((line, rowNum) => Regex.Matches(line, @"[^.\d]").Select(match => (rowNum, match.Index)))
    .ToHashSet();

bool IsTouchingPart((string, (int, int)) num) => GetSurroundingPoints(num).Any(partPositions.Contains);

IEnumerable<(int, int)> GetSurroundingPoints((string str, (int r, int c) point) num)
{
    for (int i = 0; i < num.str.Length+2; i++)
    {
        yield return (num.point.r - 1, num.point.c - 1 + i);
        yield return (num.point.r + 1, num.point.c - 1 + i);
    }
    yield return (num.point.r, num.point.c - 1);
    yield return (num.point.r, num.point.c + num.str.Length);
}

int result =
    File.ReadLines("input.txt")
    .SelectMany((line, rowNum) => Regex.Matches(line, @"\d+").Select(match => (match.Value, (rowNum, match.Index))))
    .Where(IsTouchingPart)
    .Select(t => int.Parse(t.Value))
    .Sum();

Console.WriteLine(result);