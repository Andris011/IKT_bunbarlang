namespace IKT_bunbarlang.Loverseny;

public class Loverseny
{
    private static List<Lo> lovak = new List<Lo>();

    public Loverseny(List<Lo> lovak)
    {
        lovak = Lovak;
    }

    public static List<Lo> Lovak => lovak;

    public static void LovakKiirasa()
    {
        lovak.Add(new Lo(1, "Anamoe", "2018", "James McDonald"));
        lovak.Add(new Lo(2, "Al Riffa", "2019", "Mark Zahra"));
        lovak.Add(new Lo(3, "Buckaroo", "2018", "Craig Williams"));
        lovak.Add(new Lo(4, "Arapho", "2016", "Rachel King"));
        lovak.Add(new Lo(5, "Vauban", "2017", "Blake Shinn"));
        lovak.Add(new Lo(6, "Chevalier Rose", "2017", "Damian Lane"));
        lovak.Add(new Lo(7, "Prasage Nocturne", "2019", "Stephane Pasquier"));
        lovak.Add(new Lo(8, "Meydaan", "2020", "James McDonald"));
        lovak.Add(new Lo(9, "Absurde", "2017", "Kerrin McEvoy"));
        lovak.Add(new Lo(10, "Flatten The Curve", "2018", "Thore Hammer Hansen"));

        
    }
    
    public static int ValasztasMenu()
    {
        List<string> menuOpciok = new List<string>();
        
        foreach (var lo in lovak)
        {
            // Console.WriteLine(lo);
            menuOpciok.Add(lo.ToString());
        }
        
        int valasztas = Menu.ValasztasLista(menuOpciok.ToList());

        return valasztas;
    }


    public static void Animacio()
    {
        int i = 0;
        while (i <= 8)
        {
            Console.Clear();
            if (i % 4 == 0) Console.WriteLine("A verseny folyamatban");
            if (i % 4 == 1) Console.WriteLine("A verseny folyamatban.");
            if (i % 4 == 2) Console.WriteLine("A verseny folyamatban..");
            if (i % 4 == 3) Console.WriteLine("A verseny folyamatban...");
            
            
            Thread.Sleep(1000 / 3);
            i++;
        }
    }
    
    
    public static void Play(Player player)
    {   
        Console.Clear();
        Random rnd = new Random();
        LovakKiirasa();

        bool kilepes = false;
        do
        {
            foreach (Lo elem in lovak)
            {
                elem.SzorzoHozzaadasa();
            }
            
            int nyertes = rnd.Next(0, 10);
            
            lovak[nyertes].NyertesSzorzoHozzaadasa();

            // Console.WriteLine(lovak[nyertes].ToString());
            
            int valasztas = ValasztasMenu();
            
            double bet = player.Tet();
            
            Animacio();
            
            if (nyertes == valasztas) {
                player.Nyer(bet * lovak[nyertes].Szorzo);
                Console.WriteLine("Gratulálok, nyertél!");
                Console.WriteLine($"Egyenlege {player.Zsetonok}");
            }
            else
            {
                player.Veszit(bet);
                Console.WriteLine("Sajnálom, vesztettél!");
                Console.WriteLine($"Egyenlege {player.Zsetonok}");
            }

            Console.WriteLine();
            Console.WriteLine("Nyomj entert ha szeretnél még játszani, backspace-et ha nem!");
            
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Backspace: kilepes = true; break;
                case ConsoleKey.Enter: kilepes = false; break;
            }
        } while (!kilepes);
        
        MainMenu.Show();

    }
}