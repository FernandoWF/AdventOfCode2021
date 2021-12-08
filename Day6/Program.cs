var input = File.ReadAllText("input.txt");
var parsedTimers = input.Split(',');
var timers = parsedTimers.Select(t => int.Parse(t)).ToList();

var dayCount = 80;
var timersCountAtStartOfDay = timers.Count;

for (var i = 0; i < dayCount; i++)
{
    for (var j = 0; j < timersCountAtStartOfDay; j++)
    {
        var timer = timers[j];
        if (timer == 0)
        {
            timers[j] = 6;
            timers.Add(8);
        }
        else
        {
            timers[j] -= 1;
        }
    }
    timersCountAtStartOfDay = timers.Count;
}

Console.WriteLine(timersCountAtStartOfDay);