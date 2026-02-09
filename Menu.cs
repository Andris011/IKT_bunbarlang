namespace IKT_bunbarlang;

public class Menu
{
    public static int ValasztasLista(List<string> opciok)
    {
        int valasztas = 0;
        bool vege = false;

        while (!vege)
        {
            for (int i = 0; i < opciok.Count(); i++)
            {
                string before = valasztas == i ? "> " : "  ";
                int halfWidth = (Console.WindowWidth - opciok[i].Length) / 2;

                Console.WriteLine($"{new string(' ', halfWidth)}{before}{opciok[i]}");
            }

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    valasztas = Math.Max(0, valasztas - 1);
                    break;

                case ConsoleKey.DownArrow:
                    valasztas = Math.Min(opciok.Count() - 1, valasztas + 1);
                    break;

                case ConsoleKey.Enter:
                    vege = true;
                    break;
            }

            Console.Write($"\r\x1B[{opciok.Count()}A");
        }

        for (int i = 0; i < opciok.Count(); i++)
        {
            Console.Write($"\r\x1B[2K\n");
        }

        Console.Write($"\r\x1B[{opciok.Count()}A");
        

        return valasztas;
    }
}