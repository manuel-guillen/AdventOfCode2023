// Part 1
// ========================
Func<int,int> Abs = n => n >= 0 ? n : -n;
Func<int, bool> Between(int a, int b) => a > b ? (n => n < a && n > b) : (n => n > a && n < b);

Func<((int r, int c), (int r, int c)), (int,int)> GalacticDistance(IEnumerable<int> emptyRows, IEnumerable<int> emptyCols) => t => 
    (Abs(t.Item1.r - t.Item2.r) + Abs(t.Item1.c - t.Item2.c), emptyRows.Count(Between(t.Item1.r, t.Item2.r)) + emptyCols.Count(Between(t.Item1.c, t.Item2.c)));

(int r, int c)[] locs = File.ReadLines("input.txt").SelectMany((s, i) => s.Select((c, j) => (c, j)).Where(t => t.c == '#').Select(t => (i, t.j))).ToArray();

List<int> emptyRows = Enumerable.Range(0, locs.Max(l => l.r) + 1).Except(locs.Select(l => l.r)).ToList(),
          emptyCols = Enumerable.Range(0, locs.Max(l => l.c) + 1).Except(locs.Select(l => l.c)).ToList();

var result = locs.SelectMany((loc, i) => locs.Skip(i).Select(_ => (loc, _))).Select(GalacticDistance(emptyRows, emptyCols)).Aggregate((a,b) => (a.Item1+b.Item1,a.Item2+b.Item2));
Console.WriteLine(result.Item1 + result.Item2);

// Part 2
// ========================
Console.WriteLine(999999L * result.Item2 + result.Item1);