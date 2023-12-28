// Part 1
// ========================
int[] steps = File.ReadLines("input.txt").First().Select(c => c == 'L' ? 0 : 1).ToArray();
var map = File.ReadLines("input.txt").Skip(2).ToDictionary(s => s[0..3], s => s[7..15].Split(", "));

int stepCount = 0;
for ((string loc, int i) = ("AAA", 0); loc != "ZZZ"; loc = map[loc][steps[i++ % steps.Length]]) stepCount++;
Console.WriteLine(stepCount);