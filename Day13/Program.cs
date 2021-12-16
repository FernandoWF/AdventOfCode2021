using System.Drawing;

var lines = File.ReadAllLines("input.txt");
var separatorLine = Array.IndexOf(lines, string.Empty);
var pointLines = lines[..separatorLine];
var foldInstructionLines = lines[(separatorLine + 1)..];

var points = pointLines.Select(l =>
{
    var coordinates = l.Split(',');
    return new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
});
var maxX = points.Max(p => p.X);
var maxY = points.Max(p => p.Y);

var foldInstructions = foldInstructionLines
    .Select(l =>
    {
        var rawInstruction = l[11..];
        return new FoldInstruction
        {
            Axis = rawInstruction[0] == 'x' ? Axis.X : Axis.Y,
            Coordinate = int.Parse(rawInstruction[2..])
        };
    })
    .ToList();

var paper = new bool[maxX + 1, maxY + 1];
var paperEndX = maxX;
var paperEndY = maxY;

foreach (var point in points)
{
    paper[point.X, point.Y] = true;
}

var firstInstruction = foldInstructions[0];
if (firstInstruction.Axis == Axis.X)
{
    var i = 1;
    var sourceX = firstInstruction.Coordinate + i;
    while (sourceX <= paperEndX)
    {
        var targetX = firstInstruction.Coordinate - i;
        for (var y = 0; y <= paperEndY; y++)
        {
            if (paper[sourceX, y])
            {
                paper[targetX, y] = true;
            }
        }
        sourceX = firstInstruction.Coordinate + ++i;
    }
    paperEndX = firstInstruction.Coordinate - 1;
}
else
{
    var i = 1;
    var sourceY = firstInstruction.Coordinate + i;
    while (sourceY <= paperEndY)
    {
        var targetY = firstInstruction.Coordinate - i;
        for (var x = 0; x <= paperEndX; x++)
        {
            if (paper[x, sourceY])
            {
                paper[x, targetY] = true;
            }
        }
        sourceY = firstInstruction.Coordinate + ++i;
    }
    paperEndY = firstInstruction.Coordinate - 1;
}

var visibleDots = 0;
for (var y = 0; y <= paperEndY; y++)
{
    for (var x = 0; x <= paperEndX; x++)
    {
        if (paper[x, y])
        {
            visibleDots++;
        }
    }
}

Console.WriteLine(visibleDots);

struct FoldInstruction
{
    public Axis Axis { get; init; }
    public int Coordinate { get; init; }
}

enum Axis { X, Y }