var lines = File.ReadAllLines("input.txt");
var commands = lines.Select(line =>
{
    var tokens = line.Split(' ');
    return new Command
    {
        Direction = tokens[0] switch
        {
            "forward" => Direction.Forward,
            "up" => Direction.Up,
            "down" => Direction.Down,
            _ => throw new FormatException()
        },
        Units = int.Parse(tokens[1])
    };
}).ToList();

var horizontalPosition = 0;
var depth = 0;
var aim = 0;

foreach (var command in commands)
{
    if (command.Direction == Direction.Forward)
    {
        horizontalPosition += command.Units;
        depth += command.Units * aim;
    }
    if (command.Direction == Direction.Up) { aim -= command.Units; }
    if (command.Direction == Direction.Down) { aim += command.Units; }
}

Console.WriteLine(horizontalPosition * depth);

enum Direction { Forward, Up, Down }
struct Command
{
    public Direction Direction { get; init; }
    public int Units { get; init; }
}