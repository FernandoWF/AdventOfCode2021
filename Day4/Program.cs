var lines = File.ReadAllLines("input.txt");
var numbersToCall = new Queue<int>(lines[0].Split(',').Select(n => int.Parse(n)));
var boardSideLength = 5;
var boardsCount = (lines.Length - 1) / (boardSideLength + 1);

var boards = new Board[boardsCount];
for (var i = 0; i < boardsCount; i++)
{
    boards[i] = new Board
    {
        Positions = new int[boardSideLength, boardSideLength],
        PositionMarks = new bool[boardSideLength, boardSideLength]
    };
    var startOfCorrespondingLines = 1 + i * (boardSideLength + 1);
    var indexOfFirstLineOfBoard = startOfCorrespondingLines + 1;
    for (var y = 0; y < boardSideLength; y++)
    {
        var indexOfCurrentLine = indexOfFirstLineOfBoard + y;
        var currentLine = lines[indexOfCurrentLine];
        var currentNumberIndex = 0;
        var currentNumber = int.Parse(currentLine[..2].Trim());
        boards[i].Positions![currentNumberIndex++, y] = currentNumber;

        for (var k = 2; k < currentLine.Length; k += 3)
        {
            currentNumber = int.Parse(currentLine[k..(k + 3)].Trim());
            boards[i].Positions![currentNumberIndex++, y] = currentNumber;
        }
    }
}

bool HasBoardWon(Board board)
{
    var hasWonRow = false;
    var hasWonColumn = false;
    for (var i = 0; i < boardSideLength && !hasWonRow && !hasWonColumn; i++)
    {
        hasWonRow = true;
        hasWonColumn = true;
        for (var j = 0; j < boardSideLength && (hasWonRow || hasWonColumn); j++)
        {
            if (!board.PositionMarks![j, i]) { hasWonRow = false; }

            if (!board.PositionMarks![i, j]) { hasWonColumn = false; }
        }
    }

    return hasWonRow || hasWonColumn;
}

void MarkNumber(Board board, int number)
{
    for (var y = 0; y < boardSideLength; y++)
    {
        for (var x = 0; x < boardSideLength; x++)
        {
            if (board.Positions![x, y] == number)
            {
                board.PositionMarks![x, y] = true;
                return;
            }
        }
    }
}

var winnerBoardIndex = -1;
var lastCalledNumber = -1;
do
{
    var calledNumber = numbersToCall.Dequeue();
    for (var i = 0; i < boards!.Length; i++)
    {
        MarkNumber(boards[i], calledNumber);
        if (HasBoardWon(boards[i]))
        {
            winnerBoardIndex = i;
            lastCalledNumber = calledNumber;
            break;
        }
    }
}
while (winnerBoardIndex == -1);

var winnerBoard = boards[winnerBoardIndex];
var sum = 0;
for (var y = 0; y < boardSideLength; y++)
{
    for (var x = 0; x < boardSideLength; x++)
    {
        if (!winnerBoard.PositionMarks![x, y])
        {
            sum += winnerBoard.Positions![x, y];
        }
    }
}

Console.WriteLine(sum * lastCalledNumber);

class Board
{
    public int[,]? Positions { get; init; }
    public bool[,]? PositionMarks { get; init; }
}