var parsedDepths = File.ReadAllLines("input.txt");
var depths = parsedDepths.Select(p => int.Parse(p)).ToList();

var increasedMeasurements = 0;
for (int currentDepth = 1; currentDepth < depths.Count; currentDepth++)
{
    if (depths[currentDepth] > depths[currentDepth - 1]) { increasedMeasurements++; }
}

Console.WriteLine(increasedMeasurements);