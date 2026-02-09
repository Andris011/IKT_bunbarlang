namespace IKT_bunbarlang;

public class Player
{
    private int zsetonok;

    public int Zsetonok
    {
        get => zsetonok;
        set => zsetonok = value;
    }

    public Player(int zsetonok)
    {
        this.zsetonok = zsetonok;
    }

    public override string ToString()
    {
        return $"Játékos(zsetonok={zsetonok})";
    }
}