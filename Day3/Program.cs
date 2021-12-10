var lines = File.ReadAllLines("input.txt");
var numbers = lines.Select(l => l.Select(b => int.Parse(b.ToString()))
                                 .ToArray())
                   .ToArray();

var generatorRating = 0;
var scrubberRating = 0;
var remainingGeneratorNumbers = new List<int[]>(numbers);
var remainingScrubberNumbers = new List<int[]>(numbers);

for (var i = 0; i < numbers[0].Length && (remainingGeneratorNumbers.Count > 1 || remainingScrubberNumbers.Count > 1); i++)
{
    if (remainingGeneratorNumbers.Count > 1)
    {
        var zeroCount = 0;
        for (var j = 0; j < remainingGeneratorNumbers.Count; j++)
        {
            if (remainingGeneratorNumbers[j][i] == 0) { zeroCount++; }
        }

        if (zeroCount > remainingGeneratorNumbers.Count / 2)
        {
            remainingGeneratorNumbers = remainingGeneratorNumbers.Where(n => n[i] == 0).ToList();
        }
        else
        {
            remainingGeneratorNumbers = remainingGeneratorNumbers.Where(n => n[i] == 1).ToList();
        }
    }

    if (remainingScrubberNumbers.Count > 1)
    {
        var zeroCount = 0;
        for (var j = 0; j < remainingScrubberNumbers.Count; j++)
        {
            if (remainingScrubberNumbers[j][i] == 0) { zeroCount++; }
        }

        if (zeroCount > remainingScrubberNumbers.Count / 2)
        {
            remainingScrubberNumbers = remainingScrubberNumbers.Where(n => n[i] == 1).ToList();
        }
        else
        {
            remainingScrubberNumbers = remainingScrubberNumbers.Where(n => n[i] == 0).ToList();
        }
    }
}

var generatorNumber = remainingGeneratorNumbers.Single();
var scrubberNumber = remainingScrubberNumbers.Single();

for (var i = 0; i < generatorNumber.Length; i++)
{
    generatorRating |= generatorNumber[i] << (generatorNumber.Length - 1 - i);
}

for (var i = 0; i < scrubberNumber.Length; i++)
{
    scrubberRating |= scrubberNumber[i] << (scrubberNumber.Length - 1 - i);
}

Console.WriteLine(generatorRating * scrubberRating);