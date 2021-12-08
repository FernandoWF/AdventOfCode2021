var input = File.ReadAllText("input.txt");
var parsedTimers = input.Split(',');
var timers = parsedTimers.Select(t => int.Parse(t)).ToList();

var dayCount = 256;
var timerValueToCountOnValue = new Dictionary<int, long>();

foreach (var timer in timers)
{
    if (!timerValueToCountOnValue.ContainsKey(timer))
    {
        timerValueToCountOnValue.Add(timer, 1);
    }
    else
    {
        timerValueToCountOnValue[timer]++;
    }
}

for (int i = 0; i <= 8; i++)
{
    if (!timerValueToCountOnValue.ContainsKey(i))
    {
        timerValueToCountOnValue.Add(i, 0);
    }
}

for (var i = 0; i < dayCount; i++)
{
    var timersOnNextValue = 0L;
    var timersOnCurrentValue = timerValueToCountOnValue[8];
    for (var j = 8; j > 0; j--)
    {
        timersOnNextValue = timerValueToCountOnValue[j - 1];
        timerValueToCountOnValue[j - 1] = timersOnCurrentValue;
        timersOnCurrentValue = timersOnNextValue;
    }

    timerValueToCountOnValue[6] += timersOnNextValue;
    timerValueToCountOnValue[8] = timersOnNextValue;
}

Console.WriteLine(timerValueToCountOnValue.Values.Sum());