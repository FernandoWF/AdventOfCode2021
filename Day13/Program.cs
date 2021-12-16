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

foreach (var instruction in foldInstructions)
{
    if (instruction.Axis == Axis.X)
    {
        var i = 1;
        var sourceX = instruction.Coordinate + i;
        while (sourceX <= paperEndX)
        {
            var targetX = instruction.Coordinate - i;
            for (var y = 0; y <= paperEndY; y++)
            {
                if (paper[sourceX, y])
                {
                    paper[targetX, y] = true;
                }
            }
            sourceX = instruction.Coordinate + ++i;
        }
        paperEndX = instruction.Coordinate - 1;
    }
    else
    {
        var i = 1;
        var sourceY = instruction.Coordinate + i;
        while (sourceY <= paperEndY)
        {
            var targetY = instruction.Coordinate - i;
            for (var x = 0; x <= paperEndX; x++)
            {
                if (paper[x, sourceY])
                {
                    paper[x, targetY] = true;
                }
            }
            sourceY = instruction.Coordinate + ++i;
        }
        paperEndY = instruction.Coordinate - 1;
    }
}

for (var y = 0; y <= paperEndY; y++)
{
    for (var x = 0; x <= paperEndX; x++)
    {
        Console.Write(paper[x, y] ? "#" : ".");
    }
    Console.WriteLine();
}

struct FoldInstruction
{
    public Axis Axis { get; init; }
    public int Coordinate { get; init; }
}

enum Axis { X, Y }