namespace IKT_bunbarlang;

public class Player
{
    private double zsetonok;

    public double Zsetonok
    {
        get => zsetonok;
        set => zsetonok = value;
    }

    public Player(int zsetonok)
    {
        this.zsetonok = zsetonok;
    }

    public void Nyer(double osszeg)
    {
        this.zsetonok += osszeg;
    }

    public void Veszit(double osszeg)
    {
        this.zsetonok -= osszeg;
    }


    public double SafeGetBet()
    {
        Console.Write($"Mennyi pénzben akar fogadni? Jelenlegi egyenlege {zsetonok}     ");
        string? strbet = Console.ReadLine();
        double bet = 0;
        
        while (!double.TryParse(strbet, out bet))
        {
            Console.Write("Adjon meg egy helyes összeget: ");
            strbet = Console.ReadLine();
        }
        
        return bet;
    }
    
    
    public double Tet()
    {
        bool jo;
        double bet;
        
        do
        {
            jo = true;
            
            bet = SafeGetBet();
            
            if (bet >= zsetonok)
            {
                Console.WriteLine("Nincs ennyi pénze, adjon meg egy másik összeget: ");
                jo = false;
            }

            if (bet <= 0)
            {
                Console.WriteLine("A tétnek pozítívnak kell lennie");
                jo = false;
            }
        } while(!jo);
        
        return bet;
    }


    public override string ToString()
    {
        return $"Játékos(zsetonok={Math.Round(zsetonok, 2)})";
    }
}