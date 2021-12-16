using System.Collections;

var lines = File.ReadAllLines("input.txt");
var cavesToConnections = new Dictionary<string, List<string>>();
foreach (var line in lines)
{
    var caves = line.Split('-');
    
    cavesToConnections.TryAdd(caves[0], new List<string>());
    cavesToConnections[caves[0]].Add(caves[1]);

    cavesToConnections.TryAdd(caves[1], new List<string>());
    cavesToConnections[caves[1]].Add(caves[0]);
}

var paths = cavesToConnections["start"].Select(c => new Path("start", c)).ToList();
bool allPathsDiscovered;

do
{
    allPathsDiscovered = true;
    for (var i = paths.Count - 1; i >= 0; i--)
    {
        var originalPath = paths[i];
        if (originalPath.Last != "end")
        {
            var newPaths = cavesToConnections[originalPath.Last]
                .Select(p =>
                {
                    var newPath = new Path(originalPath);
                    var added = newPath.Add(p);

                    return added ? newPath : null!;
                })
                .Where(p => p != null);
            paths.RemoveAt(i);
            paths.AddRange(newPaths);
            allPathsDiscovered = false;
        }
    }
}
while (!allPathsDiscovered);

Console.WriteLine(paths.Count);

class Path : IEnumerable<string>
{
    private readonly List<string> list = new();
    private readonly HashSet<string> alreadyVisitedSmallCaves = new();

    public string Last => list.Last();

    public Path(params string[] caves) : this((IEnumerable<string>)caves) { }

    public Path(IEnumerable<string> caves)
    {
        if (caves.Count() < 2)
        {
            throw new ArgumentException("A path needs to have at least two caves.", nameof(caves));
        }

        foreach (var cave in caves)
        {
            Add(cave);
        }
    }

    public bool Add(string cave)
    {
        if (string.IsNullOrWhiteSpace(cave))
        {
            throw new ArgumentException("The specified cave is invalid.", nameof(cave));
        }

        if (char.IsLower(cave[0]))
        {
            if (alreadyVisitedSmallCaves.Contains(cave))
            {
                return false;
            }
            alreadyVisitedSmallCaves.Add(cave);
        }
        list.Add(cave);

        return true;
    }

    public IEnumerator<string> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }

    public override string ToString()
    {
        return string.Join(',', list);
    }
}