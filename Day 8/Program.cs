var entries = File.ReadAllLines("input.txt");
var values = entries.Select(e => (e[..(e.IndexOf("|") - 1)], e[(e.IndexOf("|") + 2)..])).ToList();

var sum = 0;
foreach (var value in values)
{
    sum += GetOutputValue(value.Item1, value.Item2);
}

Segments GetSegments(string input)
{
    var segments = Segments.None;
    foreach (var segment in input)
    {
        segments |= segment switch
        {
            'a' => Segments.A,
            'b' => Segments.B,
            'c' => Segments.C,
            'd' => Segments.D,
            'e' => Segments.E,
            'f' => Segments.F,
            'g' => Segments.G,
            _ => throw new FormatException()
        };
    }

    return segments;
}

int GetOutputValue(string patterns, string digits)
{
    var segmentsToDigit = new Dictionary<Segments, int>();
    var digit1Segments = Segments.None;
    var digit3Segments = Segments.None;
    var digit4Segments = Segments.None;
    var digit7Segments = Segments.None;
    var digitsWithFiveSegments = new List<Segments>();
    var digitsWithSixSegments = new List<Segments>();

    foreach (var pattern in patterns.Split(" "))
    {
        var segments = GetSegments(pattern);

        if (pattern.Length == 2 && !segmentsToDigit.ContainsKey(segments))
        {
            segmentsToDigit.Add(segments, 1);
            digit1Segments = segments;
        }
        else if (pattern.Length == 3 && !segmentsToDigit.ContainsKey(segments))
        {
            segmentsToDigit.Add(segments, 7);
            digit7Segments = segments;
        }
        else if (pattern.Length == 4 && !segmentsToDigit.ContainsKey(segments))
        {
            segmentsToDigit.Add(segments, 4);
            digit4Segments = segments;
        }
        else if (pattern.Length == 5 && !segmentsToDigit.ContainsKey(segments))
        {
            digitsWithFiveSegments.Add(segments);
        }
        else if (pattern.Length == 6 && !segmentsToDigit.ContainsKey(segments))
        {
            digitsWithSixSegments.Add(segments);
        }
        else if (pattern.Length == 7 && !segmentsToDigit.ContainsKey(segments))
        {
            segmentsToDigit.Add(segments, 8);
        }
    }

    var segmentInDigit7ButNotInDigit1 = digit7Segments & ~digit1Segments;
    var topSegment = segmentInDigit7ButNotInDigit1;

    var undiscoveredDigitsWithFiveSegments = new List<Segments>
    {
        digitsWithFiveSegments[0],
        digitsWithFiveSegments[1],
        digitsWithFiveSegments[2]
    };

    var missingSegmentsOnFirstDigitWithFiveSegments = Segments.All & ~undiscoveredDigitsWithFiveSegments[0];
    var missingSegmentsOnSecondDigitWithFiveSegments = Segments.All & ~undiscoveredDigitsWithFiveSegments[1];
    var missingSegmentsOnThirdDigitWithFiveSegments = Segments.All & ~undiscoveredDigitsWithFiveSegments[2];

    if (!undiscoveredDigitsWithFiveSegments[1].HasFlag(missingSegmentsOnFirstDigitWithFiveSegments)
        && !undiscoveredDigitsWithFiveSegments[2].HasFlag(missingSegmentsOnFirstDigitWithFiveSegments))
    {
        segmentsToDigit.Add(undiscoveredDigitsWithFiveSegments[0], 3);
        digit3Segments = undiscoveredDigitsWithFiveSegments[0];
        undiscoveredDigitsWithFiveSegments.RemoveAt(0);
    }
    else if (!undiscoveredDigitsWithFiveSegments[0].HasFlag(missingSegmentsOnSecondDigitWithFiveSegments)
        && !undiscoveredDigitsWithFiveSegments[2].HasFlag(missingSegmentsOnSecondDigitWithFiveSegments))
    {
        segmentsToDigit.Add(undiscoveredDigitsWithFiveSegments[1], 3);
        digit3Segments = undiscoveredDigitsWithFiveSegments[1];
        undiscoveredDigitsWithFiveSegments.RemoveAt(1);
    }
    else if (!undiscoveredDigitsWithFiveSegments[0].HasFlag(missingSegmentsOnThirdDigitWithFiveSegments)
        && !undiscoveredDigitsWithFiveSegments[1].HasFlag(missingSegmentsOnThirdDigitWithFiveSegments))
    {
        segmentsToDigit.Add(undiscoveredDigitsWithFiveSegments[2], 3);
        digit3Segments = undiscoveredDigitsWithFiveSegments[2];
        undiscoveredDigitsWithFiveSegments.RemoveAt(2);
    }
    else
    {
        throw new Exception();
    }

    var segmentDigit4HaveButDigit3Doesnt = digit4Segments & Segments.All & ~digit3Segments;
    var firstLeftSegment = segmentDigit4HaveButDigit3Doesnt;

    var centerSegment = digit4Segments & Segments.All & ~digit1Segments & ~firstLeftSegment;

    if (undiscoveredDigitsWithFiveSegments[0].HasFlag(topSegment | firstLeftSegment | centerSegment))
    {
        segmentsToDigit.Add(undiscoveredDigitsWithFiveSegments[0], 5);
        segmentsToDigit.Add(undiscoveredDigitsWithFiveSegments[1], 2);
    }
    else if (undiscoveredDigitsWithFiveSegments[1].HasFlag(topSegment | firstLeftSegment | centerSegment))
    {
        segmentsToDigit.Add(undiscoveredDigitsWithFiveSegments[0], 2);
        segmentsToDigit.Add(undiscoveredDigitsWithFiveSegments[1], 5);
    }
    else
    {
        throw new Exception();
    }

    var undiscoveredDigitsWithSixSegments = new List<Segments>
    {
        digitsWithSixSegments[0],
        digitsWithSixSegments[1],
        digitsWithSixSegments[2]
    };

    if (!undiscoveredDigitsWithSixSegments[0].HasFlag(centerSegment))
    {
        segmentsToDigit.Add(digitsWithSixSegments[0], 0);
        undiscoveredDigitsWithSixSegments.RemoveAt(0);
    }
    else if (!undiscoveredDigitsWithSixSegments[1].HasFlag(centerSegment))
    {
        segmentsToDigit.Add(digitsWithSixSegments[1], 0);
        undiscoveredDigitsWithSixSegments.RemoveAt(1);
    }
    else if (!undiscoveredDigitsWithSixSegments[2].HasFlag(centerSegment))
    {
        segmentsToDigit.Add(digitsWithSixSegments[2], 0);
        undiscoveredDigitsWithSixSegments.RemoveAt(2);
    }
    else
    {
        throw new Exception();
    }

    if (undiscoveredDigitsWithSixSegments[0].HasFlag(digit7Segments))
    {
        segmentsToDigit.Add(undiscoveredDigitsWithSixSegments[0], 9);
        segmentsToDigit.Add(undiscoveredDigitsWithSixSegments[1], 6);
    }
    else if (undiscoveredDigitsWithSixSegments[1].HasFlag(digit7Segments))
    {
        segmentsToDigit.Add(undiscoveredDigitsWithSixSegments[0], 6);
        segmentsToDigit.Add(undiscoveredDigitsWithSixSegments[1], 9);
    }
    else
    {
        throw new Exception();
    }

    var outputValue = 0;
    var factor = 1000;
    foreach (var digit in digits.Split(" "))
    {
        var segments = GetSegments(digit);
        outputValue += segmentsToDigit[segments] * factor;
        factor /= 10;
    }

    return outputValue;
}

Console.WriteLine(sum);

[Flags]
enum Segments
{
    None = 0,
    A = 1,
    B = 2,
    C = 4,
    D = 8,
    E = 16,
    F = 32,
    G = 64,
    All = A | B | C | D | E | F | G
}