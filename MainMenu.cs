namespace IKT_bunbarlang;

public class MainMenu
{   
    public static void Show()
    {
        Player player = new Player(2001911);

        string[] menuOpciok = { "Lóverseny", "Rulett", "Black Jack" };

        int valasztas = Menu.ValasztasLista(menuOpciok.ToList());

        switch (valasztas)
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
        }
    }
}