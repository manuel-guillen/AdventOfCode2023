// Part 1
// ========================
int CardScore(char c) => c switch
{
    'T' => 10,
    'J' => 11,
    'Q' => 12,
    'K' => 13,
    'A' => 14,
    _ => c - '0'
};

int HandScore(string hand)
{
    var counts = hand.GroupBy(c => c).Select(g => g.Count()).ToArray();

    if (counts.Contains(5))             return 6;                            // Five of a kind
    if (counts.Contains(4))             return 5;                            // Four of a kind
    if (counts.Contains(3))             return counts.Contains(2) ? 4 : 3;   // Full House or Three of a kind
    return counts.Count(n => n == 2);                                        // Two pairs, One pair, or No pair
}

int HandStrength(string[] line) => line[0].Select(CardScore).Prepend(HandScore(line[0])).Aggregate((s,n) => 15*s + n);

int result = File.ReadLines("input.txt").Select(line => line.Split(' ')).OrderBy(HandStrength).Select((t, index) => int.Parse(t[1]) * (index+1)).Sum();
Console.WriteLine(result);