using System.Drawing;

var input = File.ReadAllLines("input.txt");
var lines = input.Select(inputLine =>
{
    var rawCoordinates = inputLine.Split(" -> ");
    var coordinates = rawCoordinates[0].Split(',');
    var a = new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
    coordinates = rawCoordinates[1].Split(',');
    var b = new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]));

    return new Line { A = a, B = b };
}).ToList();
var nonDiagonalLines = lines.Where(l => l.DeltaX == 0 || l.DeltaY == 0).ToList();
var maxX = nonDiagonalLines.Max(l => Math.Max(l.A.X, l.B.X));
var maxY = nonDiagonalLines.Max(l => Math.Max(l.A.Y, l.B.Y));

var board = new int[maxX + 1, maxY + 1];
foreach (var line in nonDiagonalLines)
{
    if (line.DeltaX == 0)
    {
        // Vertical line or a point
        if (line.DeltaY < 0)
        {
            // Upward
            for (var i = line.A.Y; i >= line.B.Y; i--)
            {
                board[line.A.X, i]++;
            }
        }
        else
        {
            // Downward
            for (var i = line.A.Y; i <= line.B.Y; i++)
            {
                board[line.A.X, i]++;
            }
        }
    }
    else
    {
        // Horizontal line
        if (line.DeltaX < 0)
        {
            // Backward
            for (var i = line.A.X; i >= line.B.X; i--)
            {
                board[i, line.A.Y]++;
            }
        }
        else
        {
            // Forward
            for (var i = line.A.X; i <= line.B.X; i++)
            {
                board[i, line.A.Y]++;
            }
        }
    }
}

var moreThanOneOverlaps = 0;
for (var y = 0; y <= maxY; y++)
{
    for (var x = 0; x <= maxX; x++)
    {
        if (board[x, y] > 1)
        {
            moreThanOneOverlaps++;
        }
    }
}
Console.WriteLine(moreThanOneOverlaps);

struct Line
{
    public Point A { get; init; }
    public Point B { get; init; }
    public int DeltaX => B.X - A.X;
    public int DeltaY => B.Y - A.Y;
}