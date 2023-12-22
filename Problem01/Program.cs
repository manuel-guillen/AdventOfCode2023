using System.Text.RegularExpressions;

// Part 1
// ========================
int ExtractValue(string line) => 10 * (line.First(Char.IsDigit) - '0') + line.Last(Char.IsDigit) - '0';

int result = File.ReadLines("input.txt").Select(ExtractValue).Sum();
Console.WriteLine(result);

// Part 2
// ========================
string digitPattern = @"one|two|three|four|five|six|seven|eight|nine|\d";
Regex firstInstanceRegex = new Regex($"^.*?({digitPattern})");
Regex lastInstanceRegex = new Regex($"^.*({digitPattern})");

int ExtractDigit(string s) => s switch
{
    "one" => 1,
    "two" => 2,
    "three" => 3,
    "four" => 4,
    "five" => 5,
    "six" => 6,
    "seven" => 7,
    "eight" => 8,
    "nine" => 9,
    _ => s[0] - '0'
};

int ExtractValue2(string line)
{
    string firstDigit = firstInstanceRegex.Match(line).Groups[1].Value;
    string lastDigit = lastInstanceRegex.Match(line).Groups[1].Value;
    return 10 * ExtractDigit(firstDigit) + ExtractDigit(lastDigit);
}

int result2 = File.ReadLines("input.txt").Select(ExtractValue2).Sum();
Console.WriteLine(result2);