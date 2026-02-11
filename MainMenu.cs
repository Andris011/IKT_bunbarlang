using IKT_bunbarlang.Utility;

namespace IKT_bunbarlang;

public class MainMenu
{
    private static string bunbarlangLogo = """ 
                                            ▄▄▄▄    █    ██  ███▄    █  ▄▄▄▄    ▄▄▄       ██▀███   ██▓    ▄▄▄       ███▄    █   ▄████ 
                                           ▓█████▄  ██  ▓██▒ ██ ▀█   █ ▓█████▄ ▒████▄    ▓██ ▒ ██▒▓██▒   ▒████▄     ██ ▀█   █  ██▒ ▀█▒
                                           ▒██▒ ▄██▓██  ▒██░▓██  ▀█ ██▒▒██▒ ▄██▒██  ▀█▄  ▓██ ░▄█ ▒▒██░   ▒██  ▀█▄  ▓██  ▀█ ██▒▒██░▄▄▄░
                                           ▒██░█▀  ▓▓█  ░██░▓██▒  ▀███▒▒██░█▀  ░██▄▄▄▄██ ▒██▀▀█▄  ▒██░   ░██▄▄▄▄██ ▓██▒  ▀███▒░▓█  ██▓
                                           ░▓█  ▀█▓▒▒█████▓ ▒██░   ▓██░░▓█  ▀█▓ ▓█   ▓██▒░██▓ ▒██▒░██████▒▓█   ▓██▒▒██░   ▓██░░▒▓███▀▒
                                           ░▒▓███▀▒░▒▓▒ ▒ ▒ ░ ▒░   ▒ ▒ ░▒▓███▀▒ ▒▒   ▓▒█░░ ▒▓ ░▒▓░░ ▒░▓  ░▒▒   ▓▒█░░ ▒░   ▒ ▒  ░▒   ▒ 
                                           ▒░▒   ░ ░░▒░ ░ ░ ░ ░░   ░ ▒░▒░▒   ░   ▒   ▒▒ ░  ░▒ ░ ▒░░ ░ ▒  ░ ▒   ▒▒ ░░ ░░   ░ ▒░  ░   ░ 
                                            ░    ░  ░░░ ░ ░    ░   ░ ░  ░    ░   ░   ▒     ░░   ░   ░ ░    ░   ▒      ░   ░ ░ ░ ░   ░ 
                                            ░         ░              ░  ░            ░  ░   ░         ░  ░     ░  ░         ░       ░
                                           """;

    private static string gameOverLogo = """
                                          ██▒   █▓▓█████   ██████ ▒███████▒▄▄▄█████▓▓█████▄▄▄█████▓▄▄▄█████▓▓█████  ██▓    
                                         ▓██░   █▒▓█   ▀ ▒██    ▒ ▒ ▒ ▒ ▄▀░▓  ██▒ ▓▒▓█   ▀▓  ██▒ ▓▒▓  ██▒ ▓▒▓█   ▀ ▓██▒    
                                          ▓██  █▒░▒███   ░ ▓██▄   ░ ▒ ▄▀▒░ ▒ ▓██░ ▒░▒███  ▒ ▓██░ ▒░▒ ▓██░ ▒░▒███   ▒██░    
                                           ▒██ █░░▒▓█  ▄   ▒   ██▒  ▄▀▒   ░░ ▓██▓ ░ ▒▓█  ▄░ ▓██▓ ░ ░ ▓██▓ ░ ▒▓█  ▄ ▒██░    
                                            ▒▀█░  ░▒████▒▒██████▒▒▒███████▒  ▒██▒ ░ ░▒████▒ ▒██▒ ░   ▒██▒ ░ ░▒████▒░██████▒
                                            ░ ▐░  ░░ ▒░ ░▒ ▒▓▒ ▒ ░░▒▒ ▓░▒░▒  ▒ ░░   ░░ ▒░ ░ ▒ ░░     ▒ ░░   ░░ ▒░ ░░ ▒░▓  ░
                                            ░ ░░   ░ ░  ░░ ░▒  ░ ░░░▒ ▒ ░ ▒    ░     ░ ░  ░   ░        ░     ░ ░  ░░ ░ ▒  ░
                                              ░░     ░   ░  ░  ░  ░ ░ ░ ░ ░  ░         ░    ░        ░         ░     ░ ░   
                                               ░     ░  ░      ░    ░ ░                ░  ░                    ░  ░    ░  ░
                                              ░                   ░                                                        
                                         """;
    
    private static int logoWidth = StringUtil.StringLength(bunbarlangLogo.Split('\n')[0]);

    private static string GetGameCard(int width, int height, int lineWithText, string text)
    {
        string lines = new string('═', width - 4);
        string empty = new string(' ', width - 4);

        string output = $"╔{lines}╗▄ ";

        for (int i = 0; i < height - 2; i++)
        {
            string content = empty;

            if (i == lineWithText)
            {
                content = text;
            }

            output += $"\n║{StringUtil.CenterString(width - 4, content)}║█░";
        }

        return output;
    }

    private static string GetMenuCard(string text, bool selected)
    {
        int width = 30;
        int height = 5;
        int expandedHeight = 10;
        int lineWithText = 10;

        if (selected)
        {
            height = expandedHeight;
            lineWithText = 4;
        }

        string emptyContent = new string(' ', width);
        string card = GetGameCard(width, height, lineWithText, text);

        if (!selected)
        {
            for (int i = height; i < expandedHeight; i++)
            {
                card = emptyContent + "\n" + card;
            }
        }

        return card;
    }

    private static void TimedConsoleWrite(int timeout, string text)
    {
        foreach (string line in text.Split('\n'))
        {
            Console.WriteLine(line);
            Thread.Sleep(timeout);
        }
    }

    private static void DrawMenu(string[] menuOpciok, int choice)
    {
        string content = GetMenuCard(menuOpciok[0], choice == 0);

        for (int i = 1; i < menuOpciok.Length; i++)
        {
            if (menuOpciok[i] != null)
            {
                content = StringUtil.CombineMultiLine(content, GetMenuCard(menuOpciok[i], choice == i), "  ");
            }
        }


        Console.WriteLine(new string('\n', 250));
        Console.WriteLine(StringUtil.CenterMultiLine(Console.WindowWidth,
            "Csikszentmihályi Döme, Fébert András és Fata Dávid bemutatja: ".PadRight(logoWidth)));
        Console.WriteLine(StringUtil.CenterMultiLine(Console.WindowWidth, bunbarlangLogo));
        Console.WriteLine(new string('\n', 5));
        Console.WriteLine(StringUtil.CenterMultiLine(Console.WindowWidth, content));
    }

    private static void DrawMenuAnimated(string[] menuOpciok, int logoAnimationTime, int cardAnimationTime)
    {
        Console.WriteLine(new string('\n', 250));
        Console.Write("\x1B[25A");
        TimedConsoleWrite(logoAnimationTime, StringUtil.CenterMultiLine(Console.WindowWidth,
            "Csikszentmihályi Döme, Fébert András és Fata Dávid bemutatja: ".PadRight(logoWidth)));
        TimedConsoleWrite(logoAnimationTime, StringUtil.CenterMultiLine(Console.WindowWidth, bunbarlangLogo));

        string[] animationOptions = new string[menuOpciok.Length];

        for (int i = 0; i < menuOpciok.Length; i++)
        {
            animationOptions[i] = menuOpciok[i];
            DrawMenu(animationOptions, -1);

            Thread.Sleep(cardAnimationTime);
        }
    }

    public static void GameOverScreen()
    {
        Console.WriteLine(new string('\n', 250));
        TimedConsoleWrite(250, StringUtil.CenterMultiLine(Console.WindowWidth, gameOverLogo));
        Console.WriteLine(new string('\n', 10));
        Thread.Sleep(1000);
    }

    public static void Show()
    {
        Console.WriteLine("Köszöntelek a bűnbarlangban!");
        Console.WriteLine("Nyomj meg bármilyen gombot a kezdéshez!");
        Console.ReadKey(true);
        Console.Write("Írja be a játék nevét (Lóverseny, Rulett, Black Jack vagy Kilépés): ");
        Thread.Sleep(2000);
        Console.WriteLine("\nVicc volt, jöhet az igazi menü?");
        Thread.Sleep(2000);
        Console.Clear();

        Player player = new Player(10000);

        string[] menuOpciok = { "Lóverseny", "Rulett", "Black Jack", "Adatok", "Kilépés" };

        int firstLoadTime = 200;
        bool firstLoad = true;

        bool running = true;
        int choice = 0;

        do
        {
            if (firstLoad)
            {
                firstLoad = false;

                DrawMenuAnimated(menuOpciok, 250, 1000);
            }

            DrawMenu(menuOpciok, choice);

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.LeftArrow:
                    choice = (choice - 1 + menuOpciok.Length) % menuOpciok.Length;
                    break;

                case ConsoleKey.RightArrow:
                    choice = (choice + 1) % menuOpciok.Length;
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();

                    switch (choice)
                    {
                        case 0:
                            Loverseny.Loverseny.Play(player);
                            break;

                        case 1:
                            Rulette.Rulette.Play(player);
                            break;

                        case 2:
                            Black_Jack.BlackJack.Play(player);
                            break;

                        case 3:
                            string adatok = "======== Adatok ========\n\n" +
                                            $"Zsetonszám: {player.Zsetonok}\n" +
                                            $"Legmagasabb zsetonszám: {player.LegmagasabbZsetonok}";
                            
                            Console.Write(new string('\n', 12));
                            Console.Write(StringUtil.CenterMultiLine(Console.WindowWidth, adatok));
                            Console.ReadKey(true);
                            break;

                        case 4:
                            running = false;
                            break;
                    }

                    break;
            }
            
            
            if (player.Vesztett)
            {
                running = false;
                GameOverScreen();
            }
        } while (running);
    }
}