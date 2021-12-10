using System.Drawing;

var lines = File.ReadAllLines("input.txt");
var width = lines[0].Length;
var height = lines.Length;

var map = new int[width, height];
var lowPoints = new List<Point>();

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
            lowPoints.Add(new Point(x, y));
        }
    }
}

List<Point> GetBasinHigherNeighbors(Point point)
{
    var basinHigherNeighbors = new List<Point>();
    if (point.X > 0 && map![point.X, point.Y] < map[point.X - 1, point.Y] && map[point.X - 1, point.Y] < 9)
    {
        var neighbor = new Point(point.X - 1, point.Y);
        basinHigherNeighbors.Add(neighbor);
        basinHigherNeighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }
    if (point.X < width - 1 && map![point.X, point.Y] < map[point.X + 1, point.Y] && map[point.X + 1, point.Y] < 9)
    {
        var neighbor = new Point(point.X + 1, point.Y);
        basinHigherNeighbors.Add(neighbor);
        basinHigherNeighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }
    if (point.Y > 0 && map![point.X, point.Y] < map[point.X, point.Y - 1] && map[point.X, point.Y - 1] < 9)
    {
        var neighbor = new Point(point.X, point.Y - 1);
        basinHigherNeighbors.Add(neighbor);
        basinHigherNeighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }
    if (point.Y < height - 1 && map![point.X, point.Y] < map[point.X, point.Y + 1] && map[point.X, point.Y + 1] < 9)
    {
        var neighbor = new Point(point.X, point.Y + 1);
        basinHigherNeighbors.Add(neighbor);
        basinHigherNeighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }

    return basinHigherNeighbors;
}

HashSet<Point> FindBasin(Point lowPoint)
{
    var neighbors = new List<Point>();
    if (lowPoint.X > 0 && map[lowPoint.X - 1, lowPoint.Y] < 9)
    {
        var neighbor = new Point(lowPoint.X - 1, lowPoint.Y);
        neighbors.Add(neighbor);
        neighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }
    if (lowPoint.X < width - 1 && map[lowPoint.X + 1, lowPoint.Y] < 9)
    {
        var neighbor = new Point(lowPoint.X + 1, lowPoint.Y);
        neighbors.Add(neighbor);
        neighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }
    if (lowPoint.Y > 0 && map[lowPoint.X, lowPoint.Y - 1] < 9)
    {
        var neighbor = new Point(lowPoint.X, lowPoint.Y - 1);
        neighbors.Add(neighbor);
        neighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }
    if (lowPoint.Y < height - 1 && map[lowPoint.X, lowPoint.Y + 1] < 9)
    {
        var neighbor = new Point(lowPoint.X, lowPoint.Y + 1);
        neighbors.Add(neighbor);
        neighbors.AddRange(GetBasinHigherNeighbors(neighbor));
    }

    var basinPoints = new HashSet<Point> { lowPoint };
    foreach (var neighbor in neighbors)
    {
        basinPoints.Add(neighbor);
    }

    return basinPoints;
}

var basins = new HashSet<Point>[lowPoints.Count];
for (int i = 0; i < basins.Length; i++)
{
    basins[i] = FindBasin(lowPoints[i]);
}

var threeBiggestBasins = basins.OrderByDescending(basin => basin.Count).Take(3).ToList();

Console.WriteLine(threeBiggestBasins[0].Count * threeBiggestBasins[1].Count * threeBiggestBasins[2].Count);