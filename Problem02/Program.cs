// Part 1
// ========================
int GetGameId(string game) => int.Parse(game[5..game.IndexOf(':')]);
bool IsValidGame(string game) => game[(game.IndexOf(':') + 1)..].Split(';').All(IsValidRoll);
bool IsValidRoll(string roll) => roll.Split(',').Select(s => s.Trim()).All(IsValidColorPile);

bool IsValidColorPile(string colorPile) => colorPile.Split(' ') switch
{
    [string count, "red"] => int.Parse(count) <= 12,
    [string count, "green"] => int.Parse(count) <= 13,
    [string count, "blue"] => int.Parse(count) <= 14,
    _ => false
};

int result = File.ReadAllLines("input.txt").Where(IsValidGame).Select(GetGameId).Sum();
Console.WriteLine(result);