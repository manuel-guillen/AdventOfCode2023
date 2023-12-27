// Part 1
// ========================
static IEnumerable<uint[]> MapComposition(uint[][] f, uint[][] g)
{
    var criticalPoints1 = f.SelectMany(r => new[] { r[1], r[1] + r[2] });
    var criticalPoints2 = g.SelectMany(r => new[] { r[1], r[1] + r[2] }).Select(n => MapEval(n, f, inverse: true));
    var criticalPoints = criticalPoints1.Union(criticalPoints2).Order().SkipWhile(n => n == 0);

    uint point = 0;
    foreach (uint nextPoint in criticalPoints)
    {
        uint value = MapEval(MapEval(point, f), g);
        if (value != point) yield return new[] { value, point, nextPoint - point };
        point = nextPoint;
    }
}

static uint MapEval(uint n, uint[][] mapping, bool inverse = false)
{
    (int source, int dest) = inverse ? (0,1) : (1,0);
    var range = mapping.FirstOrDefault(range => range[source] <= n && n - range[source] < range[2], [0, 0, 0]);
    return range[dest] + (n - range[source]);
}

Func<string,uint[]> ParseRow = row => row.Split(" ").Select(uint.Parse).ToArray();

var input = File.ReadAllText("input.txt").Trim().Split("\n\n").Select(s => s[(s.IndexOf(':') + 2)..]);
uint[] seeds = ParseRow(input.First());
uint[][] finalMap = input.Skip(1).Select(s => s.Split("\n").Select(ParseRow).ToArray()).Aggregate((f,g) => MapComposition(f,g).ToArray()).ToArray();

uint result = seeds.Select(n => MapEval(n, finalMap)).Min();
Console.WriteLine(result);

// Part 2
// ========================
var seedRanges = seeds.Chunk(2);
var criticalPoints1 = seedRanges.Select(t => t[0]);
var criticalPoints2 = finalMap.Select(r => r[1]).Where(n => seedRanges.Any(t => t[0] < n && n < t[0] + t[1]));

uint result2 = criticalPoints1.Union(criticalPoints2).Select(n => MapEval(n, finalMap)).Min();
Console.WriteLine(result2);