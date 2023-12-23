using System.Text.RegularExpressions;

// Part 1
// ========================
Dictionary<(int,int), string> parts = 
    File.ReadLines("input.txt")
    .SelectMany((line, rowNum) => Regex.Matches(line, @"[^.\d]").Select(match => ((rowNum, match.Index), match.Value)))
    .ToDictionary();

Dictionary<(int,int), string> numbers = 
    File.ReadLines("input.txt")
    .SelectMany((line, rowNum) => Regex.Matches(line, @"\d+").Select(match => ((rowNum, match.Index), match.Value)))
    .ToDictionary();

bool IsPartNumber(KeyValuePair<(int, int), string> num) => GetSurroundingPoints(num).Any(parts.ContainsKey);

IEnumerable<(int, int)> GetSurroundingPoints(KeyValuePair<(int r, int c), string> item)
{
    for (int i = 0; i < item.Value.Length+2; i++)
    {
        yield return (item.Key.r - 1, item.Key.c - 1 + i);
        yield return (item.Key.r + 1, item.Key.c - 1 + i);
    }
    yield return (item.Key.r, item.Key.c - 1);
    yield return (item.Key.r, item.Key.c + item.Value.Length);
}

var partNumbers = numbers.Where(IsPartNumber).ToDictionary();
int result = partNumbers.Select(_ => int.Parse(_.Value)).Sum();
Console.WriteLine(result);

// Part 2
// ========================
int GetGearRatio(KeyValuePair<(int, int), string> gear)
{
    bool IsGearNumber(KeyValuePair<(int, int), string> partNumber) => GetSurroundingPoints(partNumber).Contains(gear.Key);
    var gearNums = partNumbers.Where(IsGearNumber).ToHashSet();
    return gearNums.Count == 2 ? gearNums.Select(_ => int.Parse(_.Value)).Aggregate((a,b) => a*b) : 0;
}

int result2 = parts.Where(p => p.Value == "*").Select(GetGearRatio).Sum();
Console.WriteLine(result2);