// Part 1
// ========================
int[] Differences(int[] seq) => seq.Zip(seq.Skip(1), (a, b) => b - a).ToArray();
int ExtrapolateNext(int[] seq) => seq.All(x => x == seq[0]) ? seq[0] : seq[^1] + ExtrapolateNext(Differences(seq));

var result = File.ReadLines("input.txt").Select(s => s.Split(' ').Select(int.Parse).ToArray()).Select(ExtrapolateNext).Sum();
Console.WriteLine(result);