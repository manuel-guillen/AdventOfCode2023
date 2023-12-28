// Part 1
// ========================
static long CountWaysToBeat(long T, long D) => T - 2*(long)(0.5 * T - 0.5*Math.Sqrt(T * T - 4 * D)) - 1;

var input = File.ReadLines("input.txt").Take(2).Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..].Select(int.Parse).ToArray()).ToArray();
var result = Enumerable.Range(0, input[0].Length).Select(i => CountWaysToBeat(input[0][i], input[1][i])).Aggregate((m, n) => m * n);
Console.WriteLine(result);

// Part 2
// ========================
var input2 = File.ReadLines("input.txt").Take(2).Select(s => s.Replace(" ", "")[(s.IndexOf(':') + 1)..]).Select(long.Parse).ToArray();
var result2 = CountWaysToBeat(input2[0], input2[1]);
Console.WriteLine(result2);