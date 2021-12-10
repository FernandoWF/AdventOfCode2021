var openingCharacters = new char[] { '(', '[', '{', '<' };
var closingCharacters = new char[] { ')', ']', '}', '>' };

var lines = File.ReadAllLines("input.txt");

int FindIndexOfFirstEmptyChunk(string line)
{
    for (int i = 0; i < line.Length; i++)
    {
        if (closingCharacters!.Contains(line[i])) { return i - 1; }
    }

    return -1;
}

var points = 0;
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    var chunkIndex = FindIndexOfFirstEmptyChunk(line);
    do
    {
        if (Array.IndexOf(openingCharacters, line[chunkIndex]) == Array.IndexOf(closingCharacters, line[chunkIndex + 1]))
        {
            line = line.Remove(chunkIndex, 2);
        }
        else
        {
            switch (line[chunkIndex + 1])
            {
                case ')':
                    points += 3;
                    break;
                case ']':
                    points += 57;
                    break;
                case '}':
                    points += 1197;
                    break;
                case '>':
                    points += 25137;
                    break;
            }
            break;
        }
        chunkIndex = FindIndexOfFirstEmptyChunk(line);
    }
    while (chunkIndex > -1);
}

Console.WriteLine(points);