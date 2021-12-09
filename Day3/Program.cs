var lines = File.ReadAllLines("input.txt");
var numbers = lines.Select(l => l.Select(b => int.Parse(b.ToString()))
                                 .ToArray())
                   .ToArray();

var gammaRate = 0;
var epsilonRate = 0;
for (var i = 0; i < numbers[0].Length; i++)
{
    var zeroCount = 0;
    for (var j = 0; j < numbers.Length; j++)
    {
        if (numbers[j][i] == 0) { zeroCount++; }
    }

    if (zeroCount > numbers.Length / 2)
    {
        epsilonRate |= 1 << (numbers[0].Length - 1 - i);
    }
    else
    {
        gammaRate |= 1 << (numbers[0].Length - 1 - i);
    }
}
Console.WriteLine(gammaRate * epsilonRate);