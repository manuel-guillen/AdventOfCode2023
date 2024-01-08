// Part 1
// ========================
bool ConnectsNorth(char c) => c == '|' || c == 'L' || c == 'J';
bool ConnectsSouth(char c) => c == '|' || c == '7' || c == 'F';
bool ConnectsWest(char c) => c == 'S' || c == '-' || c == 'J' || c == '7';
bool ConnectsEast(char c) => c == 'S' || c == '-' || c == 'L' || c == 'F';

bool CanMoveUp((int r, int c) t, char[][] map) => t.r > 0 && ConnectsNorth(map[t.r][t.c]) && ConnectsSouth(map[t.r - 1][t.c]);
bool CanMoveDown((int r, int c) t, char[][] map) => t.r < map.Length - 1 && ConnectsSouth(map[t.r][t.c]) && ConnectsNorth(map[t.r + 1][t.c]);
bool CanMoveLeft((int r, int c) t, char[][] map) => t.c > 0 && ConnectsWest(map[t.r][t.c]) && ConnectsEast(map[t.r][t.c - 1]);
bool CanMoveRight((int r, int c) t, char[][] map) => t.c < map[t.r].Length - 1 && ConnectsEast(map[t.r][t.c]) && ConnectsWest(map[t.r][t.c + 1]);

IEnumerable<(int r, int c)> ConnectedNodes((int r, int c) t, char[][] map)
{
    if (CanMoveUp(t, map))      yield return (t.r - 1, t.c);
    if (CanMoveDown(t, map))    yield return (t.r + 1, t.c);
    if (CanMoveLeft(t, map))    yield return (t.r, t.c - 1);
    if (CanMoveRight(t, map))   yield return (t.r, t.c + 1);
}

char[][] map = File.ReadLines("input.txt").Select(s => s.ToCharArray()).ToArray();
HashSet<(int r, int c)> expanding = map.Select((row, r) => (row, r)).Where(t => t.row.Contains('S')).Select(t => (t.r, Array.IndexOf(t.row, 'S'))).ToHashSet(),
                        visited = [];
int distance = -1;
while (expanding.Count != 0)
{
    distance++;
    visited.UnionWith(expanding);
    expanding = expanding.SelectMany(t => ConnectedNodes(t, map)).Where(t => !visited.Contains(t)).ToHashSet();
}
Console.WriteLine(distance);

// Part 2
// ========================
int count = 0;
for (int r = 0; r < map.Length; r++)
{
    Position position = Position.Exterior;
    for (int c = 0; c < map[r].Length; c++)
    {
        if (visited.Contains((r, c)))
            position = (position, map[r][c]) switch
            {
                (Position.Exterior, '|') => Position.Interior,
                (Position.Exterior, 'L') => Position.OnBoundaryInteriorOnLeft,
                (Position.Exterior, 'F') => Position.OnBoundaryInteriorOnRight,
                (Position.Interior, '|') => Position.Exterior,
                (Position.Interior, 'L') => Position.OnBoundaryInteriorOnRight,
                (Position.Interior, 'F') => Position.OnBoundaryInteriorOnLeft,
                (Position.OnBoundaryInteriorOnLeft, 'J') => Position.Exterior,
                (Position.OnBoundaryInteriorOnLeft, '7') => Position.Interior,
                (Position.OnBoundaryInteriorOnRight, 'J') => Position.Interior,
                (Position.OnBoundaryInteriorOnRight, '7') => Position.Exterior,
                (_, '-') or (_, 'S') => position,
            };
        else if (position == Position.Interior)
            count++;
    }
}
Console.WriteLine(count);

enum Position { Interior, Exterior, OnBoundaryInteriorOnLeft, OnBoundaryInteriorOnRight };