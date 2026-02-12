namespace IKT_bunbarlang.Roulette;

public class RouletteBet
{
    private int[] squares;
    private int payout;
    private double amount;

    public int Payout
    {
        get => payout;
    }

    public double Amount
    {
        get => amount;
    }

    public int SquareCount
    {
        get => squares.Length;
    }

    public int[] Squares
    {
        get => squares;
    }

    private RouletteBet(int[] squares, int payout, double amount)
    {
        this.squares = squares;
        this.payout = payout;
        this.amount = amount;
    }

    public static RouletteBet StraightUpBet(int square, double amount)
    {
        return new RouletteBet(new int[] { square }, 35, amount);
    }

    public static RouletteBet SplitBet(int square1, int square2, double amount)
    {
        return new RouletteBet(new int[] { square1, square2 }, 17, amount);
    }

    public static RouletteBet StreetBet(int street, double amount)
    {
        int[] squares = new int[3];

        for (int i = 0; i < 3; i++)
        {
            squares[i] = Roulette.GetCellNumber(i, street + 1);
        }

        return new RouletteBet(squares, 11, amount);
    }

    public static RouletteBet DoubleStreetBet(int street, double amount)
    {
        int[] squares = new int[6];
        int index = 0;

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                squares[index++] = Roulette.GetCellNumber(i, street + j + 1);
            }
        }

        return new RouletteBet(squares, 5, amount);
    }

    public static RouletteBet CornerBet(int topLeftCorner, double amount)
    {
        int[] squares = new int[4];
        int row = topLeftCorner / 12;
        int col = topLeftCorner % 12;
        
        squares[0] = Roulette.GetCellNumber(row, col);
        squares[1] = Roulette.GetCellNumber(row, col + 1);
        squares[2] = Roulette.GetCellNumber(row + 1, col);
        squares[3] = Roulette.GetCellNumber(row + 1, col + 1);
        
        return new RouletteBet(squares, 8, amount);
    }

    public static RouletteBet ColumnBet(int row, double amount)
    {
        int[] squares = new int[12];

        for (int i = 1; i < 13; i++)
        {
            squares[i - 1] = Roulette.GetCellNumber(row, i);
        }

        return new RouletteBet(squares, 2, amount);
    }

    public static RouletteBet DozenBet(int third, double amount)
    {
        int[] squares = new int[3 * 4];
        int index = 0;

        for (int i = third * 12 + 1; i < third * 12 + 13; i++)
        {
            squares[index++] = i;
        }

        return new RouletteBet(squares, 2, amount);
    }

    public static RouletteBet HalfBet(int half, double amount)
    {
        int[] squares = new int[3 * 6];
        int index = 0;

        for (int i = half * 18 + 1; i < half * 18 + 19; i++)
        {
            squares[index++] = i;
        }

        return new RouletteBet(squares, 1, amount);
    }

    public static RouletteBet ParityBet(bool even, double amount)
    {
        int[] squares = new int[18];
        int index = 0;
        int start = even ? 2 : 1;

        for (int i = start; i <= 36; i += 2)
        {
            squares[index++] = i;
        }

        return new RouletteBet(squares, 1, amount);
    }

    public static RouletteBet ColorBet(bool red, double amount)
    {
        int[] squares = new int[18];
        int index = 0;

        for (int i = 1; i <= 36; i++)
        {
            if (Roulette.redNumbers.Contains(i) && red)
            {
                squares[index++] = i;
            }
            else if (!Roulette.redNumbers.Contains(i) && !red)
            {
                squares[index++] = i;
            }
        }

        return new RouletteBet(squares, 1, amount);
    }

    public static RouletteBet SnakeBet(double amount)
    {
        return new RouletteBet(new int[] { 1, 5, 9, 12, 14, 16, 19, 23, 27, 30, 32, 34 }, 2, amount);
    }

    public override string ToString()
    {
        string squareString = "";

        for (int i = 0; i < squares.Length; i++)
        {
            if (i > 0)
            {
                squareString += ", ";
            }

            if (squares[i] == -1)
            {
                squareString += "00";
            }
            else
            {
                squareString += squares[i].ToString();
            }
        }

        return
            $"> {amount} összeg -> {amount + amount * payout} nyeremény\n> {RouletteColors.COLOR_FG_GRAY}{squareString}{RouletteColors.COLOR_RESET}";
    }
}