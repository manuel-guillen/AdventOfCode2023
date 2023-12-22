// Problem 1

static int ExtractValue(string line) => 10*(line.First(Char.IsDigit) - '0') + line.Last(Char.IsDigit) - '0';

int result = File.ReadAllLines("input.txt").Select(ExtractValue).Sum();
Console.WriteLine(result);