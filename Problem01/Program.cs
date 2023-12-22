using System.Text.RegularExpressions;

// Part 1
// ========================
int ExtractValue(string line) => 10 * (line.First(Char.IsDigit) - '0') + line.Last(Char.IsDigit) - '0';

int result = File.ReadAllLines("input.txt").Select(ExtractValue).Sum();
Console.WriteLine(result);

// Part 2
// ========================
static string Reverse(string s)
{
    char[] charArray = s.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}

string digitPattern = @"one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9|0";
Regex forwardRegex = new Regex(digitPattern);
Regex backwardRegex = new Regex(Reverse(digitPattern));

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
    string firstDigit = forwardRegex.Match(line).Value;
    string lastDigit = Reverse(backwardRegex.Match(Reverse(line)).Value);
    return 10 * ExtractDigit(firstDigit) + ExtractDigit(lastDigit);
}

int result2 = File.ReadAllLines("input.txt").Select(ExtractValue2).Sum();
Console.WriteLine(result2);