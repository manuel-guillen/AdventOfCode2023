static uint PerformMapping(uint n, uint[][] mapping)
{
    var range = mapping.FirstOrDefault(range => range[1] <= n && n - range[1] < range[2], [0, 0, 0]);
    return range[0] + (n - range[1]);
}

static string FeedNumbersIntoMap(string numbers, string map)
{
    uint[][] mapping = map.Split("\n").Select(row => row.Split(" ").Select(uint.Parse).ToArray()).ToArray();
    return string.Join(" ", numbers.Split(" ").Select(uint.Parse).Select(n => PerformMapping(n, mapping)));
}

uint result = File.ReadAllText("input.txt").Split("\n\n").Select(s => s[(s.IndexOf(':')+2)..]).Aggregate(FeedNumbersIntoMap).Split(" ").Select(uint.Parse).Min();
Console.WriteLine(result);