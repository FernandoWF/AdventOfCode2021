var lines = File.ReadAllLines("input.txt");
var width = lines[0].Length;
var height = lines.Length;

var map = new int[width, height];
var lowPoints = new List<(int x, int y)>();

for (var y = 0; y < lines.Length; y++)
{
    var line = lines[y];
    for (var x = 0; x < line.Length; x++)
    {
        map[x, y] = int.Parse(line[x].ToString());
    }
}

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        if ((x <= 0 || map[x, y] < map[x - 1, y])
            && (x >= width - 1 || map[x, y] < map[x + 1, y])
            && (y <= 0 || map[x, y] < map[x, y - 1])
            && (y >= height - 1 || map[x, y] < map[x, y + 1]))
        {
            lowPoints.Add((x, y));
        }
    }
}

var riskSum = 0;
foreach (var (x, y) in lowPoints)
{
    riskSum += map[x, y] + 1;
}

Console.WriteLine(riskSum);