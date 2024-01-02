// Part 1
// ========================
int[] steps = File.ReadLines("input.txt").First().Select(c => c == 'L' ? 0 : 1).ToArray();
var map = File.ReadLines("input.txt").Skip(2).ToDictionary(s => s[0..3], s => s[7..15].Split(", "));

int stepCount = 0;
for ((string loc, int i) = ("AAA", 0); loc != "ZZZ"; loc = map[loc][steps[i++ % steps.Length]]) stepCount++;
Console.WriteLine(stepCount);

// Part 2
// ========================
(long a, long m) ChineseRemainder((long a, long m) t1, (long a, long m) t2)
{
    (long g, (long c1, long c2)) = BezoutEuclideanAlgorithm(t1.m, t2.m);
    long m = t1.m * t2.m / g;
    long a = (t1.a * c2 * t2.m + t2.a * c1 * t1.m) / g % m;

    // Keep magnitudes low
    if (a > 0 && a > m / 2) return (a - m, m);
    if (a < 0 && a < -m / 2) return (a + m, m);
    return (a, m);
}

(long g, (long c1, long c2)) BezoutEuclideanAlgorithm(long a, long b)
{
    var (t1, t2) = (a > b) ? ((1L, 0L), (0L, 1L)) : ((0L, 1L), (1L, 0L));
    while (true)
    {
        long v1 = t1.Item1 * a + t1.Item2 * b;
        long v2 = t2.Item1 * a + t2.Item2 * b;

        if (v2 == 0) return (v1, t1);

        long q = v1 / v2;
        (t1, t2) = (t2, (t1.Item1 - q * t2.Item1, t1.Item2 - q * t2.Item2));
    }
}

HashSet<List<(string,int)>> paths = map.Keys.Where(s => s.EndsWith('A')).Select(s => new List<(string, int)>() { (s, 0) }).ToHashSet(),
                            loopedBackPaths = [];

while (paths.Count > 0)
{
    foreach (List<(string, int)> path in paths.ToList())
    {
        (string loc, int step) = path.Last();
        (string, int) next = (map[loc][steps[step % steps.Length]], step + 1);

        if (path.Any(node => node.Item1 == next.Item1 && (node.Item2 - next.Item2) % steps.Length == 0))
        {
            paths.Remove(path);
            loopedBackPaths.Add(path);
        }
        else
        {
            path.Add(next);
        }
    }
}

long stepCount2 = loopedBackPaths.Select(path => path.Last().Item2).Max();

List<(long, long)> chineseRemainderProblem = [];
foreach (List<(string, int)> path in loopedBackPaths)
{
    (string loc, int step) = path.Last();
    (string, int) next = (map[loc][steps[step % steps.Length]], step + 1);

    (string, int) startOfLoop = path.First(node => node.Item1 == next.Item1 && (node.Item2 - next.Item2) % steps.Length == 0);
    int sizeOfLoop = next.Item2 - startOfLoop.Item2;

    int currentLoopPosition = (int)((stepCount2 - startOfLoop.Item2) % sizeOfLoop);
    int exitLoopPosition = (path.FindIndex(node => node.Item1.EndsWith('Z')) - startOfLoop.Item2) % sizeOfLoop;

    chineseRemainderProblem.Add((exitLoopPosition - currentLoopPosition, sizeOfLoop));
}

(long a, long m) sol = chineseRemainderProblem.Aggregate(ChineseRemainder);
stepCount2 += sol.a >= 0 ? sol.a : sol.a + sol.m;
Console.WriteLine(stepCount2);