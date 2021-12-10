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

var incompleteLines = new List<string>();
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
            break;
        }

        chunkIndex = FindIndexOfFirstEmptyChunk(line);
        if (chunkIndex == -1)
        {
            incompleteLines.Add(lines[i]);
        }
    }
    while (chunkIndex > -1);
}

var scores = new List<long>();
var stack = new Stack<char>();
for (int i = 0; i < incompleteLines.Count; i++)
{
    var line = incompleteLines[i];
    var completionString = "";
    for (int j = 0; j < line.Length; j++)
    {
        var character = line[j];
        switch (character)
        {
            case '(':
                stack.Push(character);
                break;
            case ')':
                if (stack.Peek() == '(') { stack.Pop(); }
                else { throw new Exception(); }
                break;
            case '[':
                stack.Push(character);
                break;
            case ']':
                if (stack.Peek() == '[') { stack.Pop(); }
                else { throw new Exception(); }
                break;
            case '{':
                stack.Push(character);
                break;
            case '}':
                if (stack.Peek() == '{') { stack.Pop(); }
                else { throw new Exception(); }
                break;
            case '<':
                stack.Push(character);
                break;
            case '>':
                if (stack.Peek() == '<') { stack.Pop(); }
                else { throw new Exception(); }
                break;
        }
    }

    while (stack.Any())
    {
        var popped = stack.Pop();
        switch (popped)
        {
            case '(':
                completionString += ')';
                break;
            case '[':
                completionString += ']';
                break;
            case '{':
                completionString += '}';
                break;
            case '<':
                completionString += '>';
                break;
        }
    }

    var score = 0L;
    for (int j = 0; j < completionString.Length; j++)
    {
        score *= 5;
        switch (completionString[j])
        {
            case ')':
                score += 1;
                break;
            case ']':
                score += 2;
                break;
            case '}':
                score += 3;
                break;
            case '>':
                score += 4;
                break;
        }
    }
    scores.Add(score);
}

var middleScore = scores.OrderBy(s => s).ToList()[scores.Count / 2];
Console.WriteLine(middleScore);