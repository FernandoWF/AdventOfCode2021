var parsedDepths = File.ReadAllLines("input.txt");
var depths = parsedDepths.Select(p => int.Parse(p)).ToList();

var increasedMeasurements = 0;
for (int currentDepth = 3; currentDepth < depths.Count; currentDepth++)
{
    var previousWindowMeasurement = depths[currentDepth - 1] + depths[currentDepth - 2] + depths[currentDepth - 3];
    var currentWindowMeasurement = depths[currentDepth] + depths[currentDepth - 1] + depths[currentDepth - 2];
    if (currentWindowMeasurement > previousWindowMeasurement) { increasedMeasurements++; }
}

Console.WriteLine(increasedMeasurements);