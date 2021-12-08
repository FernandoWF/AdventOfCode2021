using Microsoft.VisualBasic.FileIO;

using var parser = new TextFieldParser("input.txt") { Delimiters = new[] { "," } };
var parsedPositions = parser.ReadFields()!;
var positions = parsedPositions.Select(p => int.Parse(p)).ToList();

var minPosition = positions.Min();
var maxPosition = positions.Max();
var positionToFuel = new Dictionary<int, int>();

for (var currentPosition = minPosition; currentPosition <= maxPosition; currentPosition++)
{
    var fuel = 0;
    foreach (var position in positions)
    {
        var distance = Math.Abs(position - currentPosition);

        // Sum of the terms of an arithmetic progression
        fuel += (1 + distance) * distance / 2;
    }
    positionToFuel.Add(currentPosition, fuel);
}

foreach (var pair in positionToFuel.OrderByDescending(p => p.Value))
{
    Console.WriteLine(pair);
}