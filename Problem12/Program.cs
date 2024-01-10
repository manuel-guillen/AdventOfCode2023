using System.Text.RegularExpressions;

IEnumerable<string> PossibleStrings(string template)
{
    if (template.Length == 0) yield return "";
    else
    {
        if (template[0] == '?')
        {
            foreach (var item in PossibleStrings(template[1..]))
            {
                yield return "." + item;
                yield return "#" + item;
            }
        }
        else
            foreach (var item in PossibleStrings(template[1..]))
                yield return template[0] + item;
    }
}

int CountArrangements(string s, int[] len) => PossibleStrings(s).Where(_ => Regex.Matches(_, @"#+").Select(m => m.Value.Length).SequenceEqual(len)).Count();

int result = File.ReadLines("input.txt").Select(s => (s[..s.IndexOf(' ')], s[(s.IndexOf(' ')+1)..].Split(',').Select(int.Parse).ToArray()))
                 .Select(a => CountArrangements(a.Item1, a.Item2))
                 .Sum();
Console.WriteLine(result);