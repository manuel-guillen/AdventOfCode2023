using System.Text.RegularExpressions;

int CountArrangements(string s, int[] len) => len.Length == 1 ? Regex.Matches(s, "(?<=^[?.]*)(?=[?#]{" + len[0] + "}[?.]*$)").Count
                                                              : Regex.Matches(s, "(?<=^[?.]*)(?=[?#]{" + len[0] + "}[?.])").Select(m => CountArrangements(s[(m.Index + len[0] + 1)..], len[1..])).Sum();

int result = File.ReadLines("input.txt").Select(s => (s[..s.IndexOf(' ')], s[(s.IndexOf(' ') + 1)..].Split(',').Select(int.Parse).ToArray()))
                 .Select(t => CountArrangements(t.Item1, t.Item2))
                 .Sum();
Console.WriteLine(result);