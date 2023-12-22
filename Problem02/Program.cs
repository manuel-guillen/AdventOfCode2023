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

// Part 2
// ========================
int GetSetPower((int, int, int) set) => set.Item1 * set.Item2 * set.Item3;
(int, int, int) GetMinimumSet(string game) => game[(game.IndexOf(':') + 1)..].Split(';').Aggregate((1,1,1), UpdateMinSet);
(int, int, int) UpdateMinSet((int, int, int) currentMinSet, string roll)
{
    (int redCount, int greenCount, int blueCount) = currentMinSet;
    foreach (string colorPile in roll.Split(',').Select(s => s.Trim()))
    {
        switch (colorPile.Split(' ')) {
            case [string amount, "red"]:
                redCount = Math.Max(redCount, int.Parse(amount));       break;
            case [string amount, "green"]:
                greenCount = Math.Max(greenCount, int.Parse(amount));   break;
            case [string amount, "blue"]:
                blueCount = Math.Max(blueCount, int.Parse(amount));     break;
        }
    };

    return (redCount, greenCount, blueCount);
}

int result2 = File.ReadAllLines("input.txt").Select(_ => GetSetPower(GetMinimumSet(_))).Sum();
Console.WriteLine(result2);