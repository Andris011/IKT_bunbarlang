namespace IKT_bunbarlang.Black_Jack;

public class BlackJack
{
    public static void Play(Player player)
    {

        List<Kartya> pakli = new List<Kartya>();

        string[] szinek = {"Pikk", "Treff", "Kőr", "Káró" };
        string[] nevek = {"2","3","4", "5", "6", "7", "8", "9", "10", "Bubi", "Dáma", "Király", "Ász" };

        foreach (string szin in szinek)
        {
            foreach (string nev in nevek)
            {
                int ertek = 0;

                if (nev == "Bubi" || nev == "Dáma" || nev == "Király")
                {
                    ertek = 10;
                }
                else if (nev == "Ász")
                {
                    ertek = 11;
                }
                else
                {
                    ertek = int.Parse(nev);
                }

                pakli.Add(new Kartya(nev, szin, ertek));
            }
        }

        foreach (Kartya kartya in pakli)
        {
            Console.WriteLine(kartya);
        }

    }
}