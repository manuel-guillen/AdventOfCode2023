// Part 1
// ========================
static int CountWaysToBeat(int time, int distance) => Enumerable.Range(0, time + 1).Select(t => t * (time - t)).Where(d => d > distance).Count();

int[][] input = File.ReadLines("input.txt").Take(2).Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..].Select(int.Parse).ToArray()).ToArray();
int result = Enumerable.Range(0, input[0].Length).Select(i => CountWaysToBeat(input[0][i], input[1][i])).Aggregate((m, n) => m * n);
Console.WriteLine(result);