var entries = File.ReadAllLines("input.txt");
entries = entries.Select(e => e[(e.IndexOf("|") + 2)..]).ToArray();
var digits = entries.SelectMany(e => e.Split(' '));

var count = 0;
foreach (var digit in digits)
{
    if (digit.Length == 2 || digit.Length == 3 || digit.Length == 4 || digit.Length == 7)
    {
        count++;
    }
}

Console.WriteLine(count);