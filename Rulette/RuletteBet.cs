namespace IKT_bunbarlang.Rulette;

public class RuletteBet
{
    private int[] squares;
    private int payout;
    private int amount;

    public int Payout
    {
        get => payout;
    }

    public int Amount
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

    private RuletteBet(int[] squares, int payout, int amount)
    {
        this.squares = squares;
        this.payout = payout;
        this.amount = amount;
    }

    public static RuletteBet StraightUpBet(int square, int amount)
    {
        return new RuletteBet(new int[] { square }, 35, amount);
    }

    public static RuletteBet SplitBet(int square1, int square2, int amount)
    {
        return new RuletteBet(new int[] { square1, square2 }, 17, amount);
    }

    public static RuletteBet StreetBet(int street, int amount)
    {
        int[] squares = new int[3];

        for (int i = 0; i < 3; i++)
        {
            squares[i] = Rulette.GetCellNumber(i, street + 1);
        }

        return new RuletteBet(squares, 11, amount);
    }

    public static RuletteBet DoubleStreetBet(int street, int amount)
    {
        int[] squares = new int[6];
        int index = 0;

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                squares[index++] = Rulette.GetCellNumber(i, street + j + 1);
            }
        }

        return new RuletteBet(squares, 5, amount);
    }

    public static RuletteBet CornerBet(int topLeftCorner, int amount)
    {
        int[] squares = new int[4];
        int row = topLeftCorner / 12;
        int col = topLeftCorner % 12;
        
        squares[0] = Rulette.GetCellNumber(row, col);
        squares[1] = Rulette.GetCellNumber(row, col + 1);
        squares[2] = Rulette.GetCellNumber(row + 1, col);
        squares[3] = Rulette.GetCellNumber(row + 1, col + 1);
        
        return new RuletteBet(squares, 8, amount);
    }

    public static RuletteBet ColumnBet(int row, int amount)
    {
        int[] squares = new int[12];

        for (int i = 1; i < 13; i++)
        {
            squares[i - 1] = Rulette.GetCellNumber(row, i);
        }

        return new RuletteBet(squares, 2, amount);
    }

    public static RuletteBet DozenBet(int third, int amount)
    {
        int[] squares = new int[3 * 4];
        int index = 0;

        for (int i = third * 12 + 1; i < third * 12 + 13; i++)
        {
            squares[index++] = i;
        }

        return new RuletteBet(squares, 2, amount);
    }

    public static RuletteBet HalfBet(int half, int amount)
    {
        int[] squares = new int[3 * 6];
        int index = 0;

        for (int i = half * 18 + 1; i < half * 18 + 19; i++)
        {
            squares[index++] = i;
        }

        return new RuletteBet(squares, 1, amount);
    }

    public static RuletteBet ParityBet(bool even, int amount)
    {
        int[] squares = new int[18];
        int index = 0;
        int start = even ? 2 : 1;

        for (int i = start; i <= 36; i += 2)
        {
            squares[index++] = i;
        }

        return new RuletteBet(squares, 1, amount);
    }

    public static RuletteBet ColorBet(bool red, int amount)
    {
        int[] squares = new int[18];
        int index = 0;

        for (int i = 1; i <= 36; i++)
        {
            if (Rulette.redNumbers.Contains(i) && red)
            {
                squares[index++] = i;
            }
            else if (!Rulette.redNumbers.Contains(i) && !red)
            {
                squares[index++] = i;
            }
        }

        return new RuletteBet(squares, 1, amount);
    }

    public static RuletteBet SnakeBet(int amount)
    {
        return new RuletteBet(new int[] { 1, 5, 9, 12, 14, 16, 19, 23, 27, 30, 32, 34 }, 2, amount);
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
            $"> {amount} összeg -> {amount + amount * payout} nyeremény\n> {RuletteColors.COLOR_FG_GRAY}{squareString}{RuletteColors.COLOR_RESET}";
    }
}