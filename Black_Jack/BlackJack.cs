namespace IKT_bunbarlang.Black_Jack;
using System.Threading;
public class BlackJack
{
    static Random r = new Random();
    static List<Kartya> kevertPakli = new List<Kartya>();

    static void kartyakeveres() { 
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

        List<Kartya> p = new List<Kartya>(pakli);
        p.AddRange(pakli);
        p.AddRange(pakli);
        p.AddRange(pakli);
        p.AddRange(pakli);
        p.AddRange(pakli);

        while (p.Count > 0)
        {
            int index = r.Next(p.Count);
            kevertPakli.Add((Kartya)p[index]);
            p.RemoveAt(index);
        }
        
        /*foreach (Kartya kartya in kevertPakli)
        {
            Console.WriteLine(kartya);
        }
        */
    }

    public static int kartyakerteke(List<Kartya> kartyak)
    {
        int osszeg = 0;

        foreach (Kartya k in kartyak)
        {
            if (k.Ertek == 11)
            {
                if (osszeg+k.Ertek > 21)
                {
                    osszeg++;
                }
                else
                {
                    osszeg += k.Ertek;
                }
            }
            else
            {
                osszeg += k.Ertek;
            }
        }

        return osszeg;
    }

    public static void Play(Player player)
    {
        Console.WriteLine("""
            ---------------------------------
            Üdvözöllek a Black Jack játékban!
            ---------------------------------

            Szabályok:
              - 6 pakli francia kártyával játszva
              - Osztó mindenképp húz 16-ra és megáll 17-nél
              - Blackjack esetén 3:2 kifizetési arány

            (Nyomj egy gombot a folytatáshoz)

            """);

        Console.ReadKey();

        bool vege = false;

        while (!vege)
        {
            kartyakeveres();

                double tet = player.SafeGetBet();
                
                while (tet > player.Zsetonok || tet <= 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Zsetonjaid száma: {player.Zsetonok}");
                    Console.Write("Kérlek add meg a tétet:");
                    tet = int.Parse(Console.ReadLine());
                    if (tet > player.Zsetonok)
                    {
                        Console.Clear();
                        Console.WriteLine("Nincs elegendő zsetonod!");
                        Thread.Sleep(500);
                        Console.Clear();
                    }
                    if (tet <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("A tét magasabb kell hogy legyen nullánál!");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                }

                List<Kartya> dealerkartyai = new List<Kartya>();
                List<Kartya> playerkartyai = new List<Kartya>();

                for ( int i = 0; i<2; i++)
                {
                    playerkartyai.Add(kevertPakli[0]);
                    kevertPakli.RemoveAt(0);
                    dealerkartyai.Add(kevertPakli[0]);
                    kevertPakli.RemoveAt(0);
                }

                string kiiras = $"""
                    Tét: {tet} 

                    Dealer kártyái
                    {dealerkartyai[0]}, [Titok]

                    Player kártyái
                    {string.Join(", ", playerkartyai)}

                    """;

                List<string> opciok = new List<string> { "Stay", "Hit", "Double" };
                int valasztashuzas = 0;

                bool huzasvege = false;

                while (!huzasvege)
                {
                    bool valasztVege = false;

                    if (kartyakerteke(playerkartyai)==21 && kartyakerteke(dealerkartyai)!=21)
                    {
                        valasztVege = true;
                    }

                    if (kartyakerteke(dealerkartyai) == 21)
                    {
                        valasztVege = true;
                    }

                    while (!valasztVege)
                    {
                        Console.Clear();
                        Console.WriteLine(kiiras);


                        for (int i = 0; i < opciok.Count; i++)
                        {
                            if (i == valasztashuzas)
                            {
                                Console.Write($"[{opciok[i]}]");
                            }
                            else
                            {
                                Console.Write($" {opciok[i]} ");
                            }
                        }
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.LeftArrow:
                                valasztashuzas = Math.Max(0, valasztashuzas - 1);
                                break;

                            case ConsoleKey.RightArrow:
                                valasztashuzas = Math.Min(opciok.Count - 1, valasztashuzas + 1);
                                break;

                            case ConsoleKey.Enter:
                                Console.Clear();
                                valasztVege = true;
                                break;
                        }
                    }

                    switch (valasztashuzas)
                    {
                        case 0:
                            kiiras = $"""
                                Tét: {tet} 

                                Dealer kártyái
                                {string.Join(", ", dealerkartyai)}

                                Player kártyái
                                {string.Join(", ", playerkartyai)}

                                """;
                            huzasvege = true;
                            break;
                        case 1:
                            playerkartyai.Add(kevertPakli[0]);
                            kevertPakli.RemoveAt(0);
                            if (opciok.Contains("Double"))
                            {
                                opciok.Remove("Double");
                            }
                            kiiras = $"""
                                Tét: {tet} 

                                Dealer kártyái
                                {dealerkartyai[0]}, [Titok]

                                Player kártyái
                                {string.Join(", ", playerkartyai)}

                                """;
                            if (kartyakerteke(playerkartyai)>21)
                            {
                                huzasvege = true;
                                kiiras = $"""
                                Tét: {tet} 

                                Dealer kártyái
                                {string.Join(", ", dealerkartyai)}

                                Player kártyái
                                {string.Join(", ", playerkartyai)}

                                """;
                            }
                            break;
                        case 2:
                            tet += tet;
                            playerkartyai.Add(kevertPakli[0]);
                            kevertPakli.RemoveAt(0);
                            kiiras = $"""
                                Tét: {tet} 

                                Dealer kártyái
                                {string.Join(", ", dealerkartyai)}

                                Player kártyái
                                {string.Join(", ", playerkartyai)}

                                """;
                            huzasvege = true;
                            break;
                    }
                }

                Console.Clear();
                Console.WriteLine(kiiras);

                if (kartyakerteke(playerkartyai) == 21 && playerkartyai.Count == 2 && kartyakerteke(dealerkartyai) != 21)
                {
                    Thread.Sleep(500);
                    Console.WriteLine();
                    Console.WriteLine("Blackjack!");
                    Thread.Sleep(500);
                    Console.WriteLine("Nyertél!");
                    Console.WriteLine();
                    player.Zsetonok += tet*1.5;
                    Thread.Sleep(500);
                }
                if (kartyakerteke(playerkartyai)>21)
                {
                    Thread.Sleep(500);
                    Console.WriteLine();
                    Console.WriteLine("Besokalltál!");
                    Thread.Sleep(500);
                    Console.WriteLine("Vesztettél!");
                    Console.WriteLine();
                    player.Zsetonok -= tet;
                    Thread.Sleep(500);
                }
                else if (kartyakerteke(dealerkartyai) == 21 && dealerkartyai.Count == 2)
                {
                    Thread.Sleep(500);
                    Console.WriteLine();
                    Console.WriteLine("Vesztettél!");
                    Console.WriteLine();
                    player.Zsetonok -= tet;
                    Thread.Sleep(500);
                }
                else
                {
                    while (kartyakerteke(dealerkartyai) < 17)
                    {
                        dealerkartyai.Add(kevertPakli[0]);
                        kevertPakli.RemoveAt(0);
                        kiiras = $"""
                        Tét: {tet} 

                        Dealer kártyái
                        {string.Join(", ", dealerkartyai)}

                        Player kártyái
                        {string.Join(", ", playerkartyai)}

                        """;
                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.WriteLine(kiiras);
                    }
                    if (kartyakerteke(dealerkartyai) > 21)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine();
                        Console.WriteLine("Dealer besokallt!");
                        Thread.Sleep(500);
                        Console.WriteLine("Nyertél!");
                        Console.WriteLine();
                        player.Zsetonok += tet;
                        Thread.Sleep(500);
                    }
                    else if (kartyakerteke(playerkartyai) > kartyakerteke(dealerkartyai))
                    {
                        Thread.Sleep(500);
                        Console.WriteLine();
                        Console.WriteLine("Nyertél!");
                        Console.WriteLine();
                        player.Zsetonok += tet;
                        Thread.Sleep(500);
                    }
                    else if (kartyakerteke(playerkartyai) == kartyakerteke(dealerkartyai))
                    {
                        Thread.Sleep(500);
                        Console.WriteLine();
                        Console.WriteLine("Döntetlen...");
                        Console.WriteLine();
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Thread.Sleep(500);
                        Console.WriteLine();
                        Console.WriteLine("Vesztettél!");
                        Console.WriteLine();
                        player.Zsetonok -= tet;
                        Thread.Sleep(500);
                    }
                }

                Thread.Sleep(1000);
                Console.WriteLine("(Nyomj egy gombot a folytatáshoz)");

                Console.ReadKey();
                
            
                Console.WriteLine();
                bool ujgame = false;
                List<string> vegeopciok = new List<string> { "Új kör", "Kilépés"};
                int valasztasvege = 0;
                while (!ujgame)
                {
                    Console.Clear();
                    Console.WriteLine("Szeretnél még játszani?");
                    Console.WriteLine();
                    for (int i = 0; i < vegeopciok.Count; i++)
                    {
                        if (i == valasztasvege)
                        {
                            Console.Write($"[{vegeopciok[i]}]");
                        }
                        else
                        {
                            Console.Write($" {vegeopciok[i]} ");
                        }
                    }
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            valasztasvege = Math.Max(0, valasztasvege - 1);
                            break;

                        case ConsoleKey.RightArrow:
                            valasztasvege = Math.Min(vegeopciok.Count - 1, valasztasvege + 1);
                            break;

                        case ConsoleKey.Enter:
                            Console.Clear();
                            ujgame = true;
                            break;
                    }
                }
                switch (valasztasvege)
                {
                    case 0:
                        if (kevertPakli.Count < 10)
                        {
                            kartyakeveres();
                        }
                        break;
                    case 1:
                        vege=true;
                        break;
                }
                vege = vege || player.Vesztett;
        }
    }
}