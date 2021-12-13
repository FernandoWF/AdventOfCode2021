var lines = File.ReadAllLines("input.txt");
var width = lines[0].Length;
var height = lines.Length;
var grid = new int[width, height];
var hasFlashedGrid = new bool[width, height];

for (var y = 0; y < lines.Length; y++)
{
    var line = lines[y];
    for (var x = 0; x < line.Length; x++)
    {
        grid[x, y] = int.Parse(line[x].ToString());
    }
}

void IncrementIfExists(int x, int y)
{
    if (x < 0 || x >= width || y < 0 || y >= height)
    {
        return;
    }

    grid![x, y]++;
}

var flashes = 0;
void FlashOctopus(int x, int y)
{
    if (x < 0 || x >= width || y < 0 || y >= height || hasFlashedGrid![x, y] || grid![x, y] <= 9)
    {
        return;
    }

    hasFlashedGrid[x, y] = true;
    flashes++;

    IncrementIfExists(x - 1, y - 1);
    FlashOctopus(x - 1, y - 1);

    IncrementIfExists(x, y - 1);
    FlashOctopus(x, y - 1);

    IncrementIfExists(x + 1, y - 1);
    FlashOctopus(x + 1, y - 1);

    IncrementIfExists(x - 1, y);
    FlashOctopus(x - 1, y);

    IncrementIfExists(x + 1, y);
    FlashOctopus(x + 1, y);

    IncrementIfExists(x - 1, y + 1);
    FlashOctopus(x - 1, y + 1);

    IncrementIfExists(x, y + 1);
    FlashOctopus(x, y + 1);

    IncrementIfExists(x + 1, y + 1);
    FlashOctopus(x + 1, y + 1);
}

void ExecuteStep()
{
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            grid![x, y]++;
        }
    }

    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            FlashOctopus(x, y);
        }
    }

    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            if (hasFlashedGrid![x, y])
            {
                grid[x, y] = 0;
                hasFlashedGrid![x, y] = false;
            }
        }
    }
}

var steps = 100;
for (var x = 0; x < steps; x++)
{
    ExecuteStep();
}

Console.WriteLine(flashes);