using IKT_bunbarlang.Utility;

namespace IKT_bunbarlang.Roulette;

public class Roulette
{
    public static int[] redNumbers = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

    private static string[] wheelNumbers =
    {
        "0", "28", "9", "26", "30", "11", "7", "20", "32", "17", "5", "22", "34", "15", "3", "24", "36", "13", "1",
        "00", "27", "10", "25", "29", "12", "8", "19", "31", "18", "6", "21", "33", "16", "4", "23", "35", "14", "2"
    };

    private static int[] GetColumnMaxLengths(string[,] board, int minCellWidth)
    {
        int[] maxLengths = new int[board.GetLength(1)];

        for (int i = 0; i < maxLengths.Length; i++)
        {
            maxLengths[i] = minCellWidth;
        }

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                maxLengths[j] = Math.Max(maxLengths[j], StringUtil.StringLength(board[i, j]));
            }
        }

        return maxLengths;
    }

    private static string DrawBoard(string[,] board, int minCellWidth)
    {
        int[] maxLengths = GetColumnMaxLengths(board, minCellWidth);
        string output = "";

        int dimensionY = board.GetLength(0);
        int dimensionX = board.GetLength(1);

        for (int i = 0; i < dimensionY; i++)
        {
            string[,] currentRow = new string[dimensionX, 3];
            string[] built = new string[] { "", "", "" };

            for (int j = 0; j < dimensionX; j++)
            {
                char topLeft = '┌';
                char bottomLeft = '└';
                char topRight = '┐';
                char bottomRight = '┘';

                string[] currentCell = { "", "", "" };
                string lines = new string('─', maxLengths[j] + 2);

                if (i < dimensionY - 1 && board[i + 1, j] != "")
                {
                    bottomLeft = '├';
                    bottomRight = '┤';
                }

                if (j < dimensionX - 1 && board[i, j + 1] != "")
                {
                    topRight = '┬';
                    bottomRight = '┴';
                }

                if (i < dimensionY - 1 && j < dimensionX - 1 && board[i + 1, j + 1] != "")
                {
                    bottomRight = '┼';
                }

                if (i < dimensionY - 1 && j > 0 && board[i + 1, j - 1] != "")
                {
                    bottomLeft = '┼';
                }

                if (board[i, j] == "")
                {
                    int padding = j == 0 ? 3 : 2;
                    string empty = new string(' ', maxLengths[j] + padding);

                    currentRow[j, 0] = empty;
                    currentRow[j, 1] = empty;
                    currentRow[j, 2] = empty;

                    if (i < dimensionY - 1 && board[i + 1, j] != "")
                    {
                        currentRow[j, 2] = $"{topLeft}{lines}";
                    }

                    continue;
                }

                if (j == 0 || board[i, j - 1] == "")
                {
                    currentCell[0] += topLeft;
                    currentCell[1] += '│';
                    currentCell[2] += bottomLeft;
                }

                currentCell[0] += $"{lines}{topRight}";
                currentCell[1] += $" {StringUtil.CenterString(maxLengths[j], board[i, j])} │";
                currentCell[2] += $"{lines}{bottomRight}";

                currentRow[j, 0] = currentCell[0];
                currentRow[j, 1] = currentCell[1];
                currentRow[j, 2] = currentCell[2];
            }

            for (int j = 0; j < dimensionX; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    built[k] += currentRow[j, k];
                }
            }

            if (i == 0)
            {
                output += built[0] + "\n";
            }

            output += built[1] + "\n";
            output += built[2] + "\n";
        }

        return output;
    }

    private static string ColorString(string color, string text)
    {
        return color + RouletteColors.COLOR_FG_WHITE + text + RouletteColors.COLOR_RESET;
    }

    public static int GetCellNumber(int row, int column)
    {
        return (column - 1) * 3 + 2 - row + 1;
    }

    public static (int, int) GetNumberCell(int number)
    {
        int column = (int)Math.Ceiling(number / 3d);
        int row = number - (column - 1) * 3 - 1;

        return (row, column);
    }

    private static string TableDrawing()
    {
        return TableDrawing(new int[] { });
    }

    private static string TableDrawing(int[] highlightedSqaures)
    {
        return TableDrawing(highlightedSqaures, new int[] { });
    }

    private static string TableDrawing(int[] highlightedSquares, int[] wonSquares)
    {
        string[,] board = new string[3, 14];

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                board[i, j] = "";
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j < 13; j++)
            {
                string color = RouletteColors.COLOR_BLACK;
                int number = GetCellNumber(i, j);

                if (redNumbers.Contains(number))
                {
                    color = RouletteColors.COLOR_RED;
                }

                if (highlightedSquares.Contains(number))
                {
                    color = RouletteColors.COLOR_GREEN;
                }

                if (wonSquares.Contains(number))
                {
                    color = RouletteColors.COLOR_YELLOW;
                }

                board[i, j] = ColorString(color, $"{number}".PadLeft(2, ' '));
            }
        }

        board[1, 0] = "00";
        board[2, 0] = "0";

        if (highlightedSquares.Contains(-1))
        {
            board[1, 0] = ColorString(RouletteColors.COLOR_GREEN, "00");
        }

        if (highlightedSquares.Contains(0))
        {
            board[2, 0] = ColorString(RouletteColors.COLOR_GREEN, "0");
        }

        if (wonSquares.Contains(-1))
        {
            board[1, 0] = ColorString(RouletteColors.COLOR_YELLOW, "00");
        }

        if (wonSquares.Contains(0))
        {
            board[2, 0] = ColorString(RouletteColors.COLOR_YELLOW, "0");
        }

        string[,] boardThirds = new string[,]
        {
            { "Első 12", "Második 12", "Harmadik 12" }
        };

        string[,] boardSixths = new string[,]
        {
            {
                "1 : 18", "Páros", ColorString(RouletteColors.COLOR_RED, "Piros"),
                ColorString(RouletteColors.COLOR_BLACK, " Fekete"), "Páratlan",
                "19 : 36"
            }
        };

        for (int i = 0; i < 6; i++)
        {
            int length = 7;

            if (i == 0)
            {
                length--;
            }

            boardSixths[0, i] = StringUtil.CenterString(length, boardSixths[0, i]);
        }

        board[0, 13] = "2 : 1";
        board[1, 13] = "2 : 1";
        board[2, 13] = "2 : 1";

        return DrawBoard(board, 2) +
               StringUtil.IndentString(DrawBoard(boardThirds, 17), 5) +
               StringUtil.IndentString(DrawBoard(boardSixths, 0), 5);
    }

    public static bool IsArgNumber(string arg)
    {
        return int.TryParse(arg, out _);
    }

    public static RouletteBet? ParseBet(string bet)
    {
        string[] arguments = bet.Split(' ');
        double betAmount = 0;

        if (arguments.Length > 2 && double.TryParse(arguments[0], out _) && (arguments[1] == "a" || arguments[1] == "on"))
        {
            betAmount = double.Parse(arguments[0]);
            arguments = arguments.Skip(2).ToArray();
        }

        string command = arguments[0];

        switch (command)
        {
            case "0":
                return RouletteBet.StraightUpBet(0, betAmount);

            case "00":
                return RouletteBet.StraightUpBet(-1, betAmount);

            case "csak":
            case "sima":
            case "plain":
            case "straight":
            case "straightup":
                if (arguments.Length != 2) return null;
                if (!IsArgNumber(arguments[1])) return null;

                int number = int.Parse(arguments[1]);
                if (number < 0 || number > 36) return null;

                return RouletteBet.StraightUpBet(number, betAmount);

            case "split":
                if (arguments.Length != 3) return null;
                if (!IsArgNumber(arguments[1]) || !IsArgNumber(arguments[2])) return null;

                int splitA = int.Parse(arguments[1]);
                int splitB = int.Parse(arguments[2]);

                (int splitARow, int splitACol) = GetNumberCell(splitA);
                (int splitBRow, int splitBCol) = GetNumberCell(splitB);

                int distance = Math.Abs(splitARow - splitBRow) + Math.Abs(splitACol - splitBCol);

                if (distance != 1) return null;

                return RouletteBet.SplitBet(splitA, splitB, betAmount);

            case "utca":
            case "street":
                if (arguments.Length != 2) return null;
                if (!IsArgNumber(arguments[1])) return null;

                int streetNumber = int.Parse(arguments[1]) - 1;
                if (streetNumber < 0 || streetNumber > 11) return null;

                return RouletteBet.StreetBet(streetNumber, betAmount);

            case "dutca":
            case "duplautca":
            case "dstreet":
            case "doublestreet":
                if (arguments.Length != 2) return null;
                if (!IsArgNumber(arguments[1])) return null;

                int doubleStreetNumber = int.Parse(arguments[1]) - 1;
                if (doubleStreetNumber < 0 || doubleStreetNumber > 10) return null;

                return RouletteBet.DoubleStreetBet(doubleStreetNumber, betAmount);

            case "square":
            case "corner":
            case "sarok":
            case "négyzet":
                if (arguments.Length != 2) return null;
                if (!IsArgNumber(arguments[1])) return null;

                int cornerNumber = int.Parse(arguments[1]) - 1;
                if (cornerNumber < 0 || cornerNumber > 21) return null;

                return RouletteBet.CornerBet(cornerNumber, betAmount);

            case "column":
            case "oszlop":
                if (arguments.Length != 2) return null;
                if (!IsArgNumber(arguments[1])) return null;

                int columnNumber = int.Parse(arguments[1]) - 1;
                if (columnNumber < 0 || columnNumber > 2) return null;

                return RouletteBet.ColumnBet(columnNumber, betAmount);

            case "dozen":
            case "tucat":
            case "harmad":
                if (arguments.Length != 2) return null;
                if (!IsArgNumber(arguments[1])) return null;

                int dozenNumber = int.Parse(arguments[1]) - 1;
                if (dozenNumber < 0 || dozenNumber > 2) return null;

                return RouletteBet.DozenBet(dozenNumber, betAmount);

            case "half":
            case "fél":
                if (arguments.Length != 2) return null;
                if (!IsArgNumber(arguments[1])) return null;

                int halfNumber = int.Parse(arguments[1]) - 1;
                if (halfNumber < 0 || halfNumber > 1) return null;

                return RouletteBet.HalfBet(halfNumber, betAmount);

            case "even":
            case "páros":
                return RouletteBet.ParityBet(true, betAmount);

            case "odd":
            case "páratlan":
                return RouletteBet.ParityBet(false, betAmount);

            case "red":
            case "piros":
                return RouletteBet.ColorBet(true, betAmount);

            case "black":
            case "fekete":
                return RouletteBet.ColorBet(false, betAmount);

            case "snake":
            case "kígyó":
                return RouletteBet.SnakeBet(betAmount);

            default:
                return null;
        }
    }

    private static int[] GetEmptyHighlighted()
    {
        int[] arr = new int[38];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = -100;
        }

        return arr;
    }

    public static RouletteBet? PlaceBet(List<RouletteBet> bets, double balance)
    {
        ConsoleKeyInfo key;
        string enteredCommand = "";
        string shownOutput = "";

        int[] selectedSquares = GetEmptyHighlighted();

        do
        {
            (string table, _) = StringUtil.StringsEqualWidth(TableDrawing(selectedSquares), "");
            string combined = StringUtil.CombineMultiLine(table,
                $"=== Fogadás ({balance} zseton) ===\n> {enteredCommand}\n{new string(' ', enteredCommand.Length + 2)}{shownOutput}",
                "  ");
            (combined, _) = StringUtil.StringsEqualWidth(combined, "");

            string output = StringUtil.CenterMultiLine(Console.WindowWidth, combined);
            string[] outputLines = output.Split('\n');

            int spaceAtEnd = enteredCommand.Length - enteredCommand.TrimEnd().Length;
            int spaceBeginning = enteredCommand.Length == 0 ? 1 : 0;

            int cursorPosition = StringUtil.StringLength(outputLines[1].TrimEnd()) + spaceAtEnd + spaceBeginning;

            Console.WriteLine($"\x1B[H{output}");
            Console.Write($"\x1B[{outputLines.Length - 1}A\x1B[{cursorPosition}C");

            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace)
            {
                if (enteredCommand.Length > 0)
                {
                    enteredCommand = enteredCommand.Remove(enteredCommand.Length - 1, 1);
                }
                else
                {
                    return null;
                }
            }
            else if (char.IsLetterOrDigit(key.KeyChar) || key.KeyChar == ' ' || key.KeyChar == ',')
            {
                enteredCommand += key.KeyChar;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                RouletteBet? submitted = ParseBet(enteredCommand);

                if (submitted != null && submitted.Amount <= balance && submitted.Amount >= 10)
                {
                    return submitted;
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                return null;
            }

            RouletteBet? parsed = ParseBet(enteredCommand);

            Console.Write($"\x1B[1B\x1B[{enteredCommand.Length}D\x1B[0K\x1B[{enteredCommand.Length}C");

            selectedSquares = GetEmptyHighlighted();
            shownOutput = "ismeretlen fogadási parancs";

            if (parsed != null)
            {
                selectedSquares = parsed.Squares;
                shownOutput = $"x{parsed.Payout} nyereség, {parsed.SquareCount} szám kiválasztva";

                if (parsed.Amount > balance)
                {
                    shownOutput = "nincs ennyi zsetonod";
                }

                if (parsed.Amount < 10)
                {
                    shownOutput = "a minimum fogadás 10 zseton";
                }
            }

            Console.Write($"{shownOutput}\x1B[{shownOutput.Length}D\x1B[1A");
        } while (true);
    }

    public static double Porgetes(List<RouletteBet> bets)
    {
        Random random = new Random();

        int radius = 9;
        int diameter = radius * 2;

        int ballOffset = random.Next(0, 37);
        int wheelOffset = random.Next(0, 37);
        int spinCycles = random.Next(50, 80);

        string winningNumber = "0"; 

        string ball = ColorString(RouletteColors.COLOR_WHITE, "o");

        do
        {
            Console.Write("\x1B[H");

            for (int i = 0; i <= diameter; i++)
            {
                int leftIndex = (37 - i + wheelOffset) % wheelNumbers.Length;
                int rightIndex = (i + wheelOffset) % wheelNumbers.Length;

                string leftNumber = ColorNumber(wheelNumbers[leftIndex].PadLeft(2, ' '));
                string rightNumber = ColorNumber(wheelNumbers[rightIndex].PadLeft(2, ' '));

                int y = i - radius;
                double x = Math.Sqrt(radius * radius - y * y);
                int padding = (int)Math.Round(x);

                string space = new string(' ', 1 + padding * 5);

                string padLeft = "";
                string padRight = "";

                if (ballOffset <= 18 && i == 18 - ballOffset)
                {
                    winningNumber = wheelNumbers[rightIndex];
                    padRight = $" {ball}";
                    padLeft = new string(' ', StringUtil.StringLength(padRight));
                }
                else if (ballOffset >= 19 && i == ballOffset - 19)
                {
                    winningNumber = wheelNumbers[leftIndex];
                    padLeft = $"{ball} ";
                    padRight = new string(' ', StringUtil.StringLength(padLeft));
                }

                Console.WriteLine(StringUtil.CenterString(Console.WindowWidth,
                    $"{padLeft}{leftNumber}{space}{rightNumber}{padRight}"));
            }

            ballOffset = (ballOffset - 1 + wheelNumbers.Length) % wheelNumbers.Length;
            wheelOffset = (wheelOffset + 1) % wheelNumbers.Length;

            int momentum = 100;

            if (--spinCycles < 10)
            {
                momentum = 250 + (10 - spinCycles) * 5;
            }

            Thread.Sleep(momentum);
        } while (spinCycles > 0);

        Thread.Sleep(2500);

        int winningParsed = winningNumber == "00" ? -1 : int.Parse(winningNumber);

        double wonAmount = 0;
        double lostAmount = 0;

        int allBetCount = bets.Count;
        int wonBetCount = 0;

        foreach (RouletteBet bet in bets)
        {
            if (bet.Squares.Contains(winningParsed))
            {
                wonBetCount++;
                wonAmount += bet.Amount + bet.Amount * bet.Payout;
            }
            else
            {
                lostAmount += bet.Amount;
            }
        }

        double profits = wonAmount - lostAmount;

        Console.Clear();

        int[] selectedSquares = GetEmptyHighlighted();
        int[] wonSquares = new int[] { winningParsed };

        int selectedIndex = 0;

        foreach (RouletteBet bet in bets)
        {
            for (int i = 0; i < bet.Squares.Length; i++)
            {
                if (!selectedSquares.Contains(bet.Squares[i]))
                {
                    selectedSquares[selectedIndex++] = bet.Squares[i];
                }
            }
        }

        bets.Clear();

        string table = TableDrawing(selectedSquares, wonSquares);
        string betsDrawn = "======== Eredmények ========\n" +
                           $"{allBetCount} fogadásból {wonBetCount} volt nyerő\n\n"+ 
                           $"+{wonAmount} nyert zseton\n" +
                           $"-{lostAmount} vesztett zseton\n\n" +
                           $"Összesítve: {profits} zseton\n";

        (table, _) = StringUtil.StringsEqualWidth(table, "");

        string output = StringUtil.CombineMultiLine(table, betsDrawn, "  ");
        (output, _) = StringUtil.StringsEqualWidth(output, "");

        Console.WriteLine(StringUtil.CenterMultiLine(Console.WindowWidth, output));
        Console.ReadKey(true);

        return wonAmount;
    }

    private static string ColorNumber(string number)
    {
        string color = RouletteColors.COLOR_BLACK;

        if (redNumbers.Contains(int.Parse(number)))
        {
            color = RouletteColors.COLOR_RED;
        }

        if (number == " 0" || number == "00")
        {
            color = RouletteColors.COLOR_GREEN;
        }

        return ColorString(color, number);
    }

    public static void Play(Player player)
    {
        string[] opciok = { "Fogadás", "Pörgetés", "Kilépés" };
        int opcio;

        List<RouletteBet> bets = new List<RouletteBet>();

        do
        {
            int[] selectedSquares = GetEmptyHighlighted();
            int selectedIndex = 0;

            foreach (RouletteBet bet in bets)
            {
                for (int i = 0; i < bet.Squares.Length; i++)
                {
                    if (!selectedSquares.Contains(bet.Squares[i]))
                    {
                        selectedSquares[selectedIndex++] = bet.Squares[i];
                    }
                }
            }

            string table = TableDrawing(selectedSquares);
            string betsDrawn = $"=== Fogadások ({player.Zsetonok} zseton) ===";

            for (int i = 0; i < bets.Count; i++)
            {
                betsDrawn += "\n" + bets[i];
            }

            (table, _) = StringUtil.StringsEqualWidth(table, "");

            string output = StringUtil.CombineMultiLine(table, betsDrawn, "  ");
            (output, _) = StringUtil.StringsEqualWidth(output, "");

            Console.Clear();
            Console.WriteLine(StringUtil.CenterMultiLine(Console.WindowWidth, output));

            opcio = Menu.ValasztasLista(opciok.ToList());

            Console.Clear();

            switch (opcio)
            {
                case 0:
                    if (bets.Count < 6)
                    {
                        RouletteBet? bet = PlaceBet(bets, player.Zsetonok);
                        Console.Clear();

                        if (bet != null)
                        {
                            bets.Add(bet);
                            player.Zsetonok -= bet.Amount;
                        }
                    }

                    break;

                case 1:
                    Console.Clear();

                    if (bets.Count > 0)
                    {
                        player.Zsetonok += Porgetes(bets);
                    }

                    break;

                case 2:
                    foreach (RouletteBet unusedBet in bets)
                    {
                        player.Zsetonok += unusedBet.Amount;
                    }

                    break;
            }

            if (player.Vesztett && bets.Count == 0)
            {
                opcio = 2;
            }
        } while (opcio != 2);
    }
}