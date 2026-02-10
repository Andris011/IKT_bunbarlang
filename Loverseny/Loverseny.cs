namespace IKT_bunbarlang.Loverseny;

public class Loverseny
{
    private static List<Lo> lovak = new List<Lo>();

    public static void LovakKiirasa()
    {
        lovak.Add(new Lo("Anamoe", "2018", "James McDonald"));
        lovak.Add(new Lo("Al Riffa", "2019", "Mark Zahra"));
        lovak.Add(new Lo("Buckaroo", "2018", "Craig Williams"));
        lovak.Add(new Lo("Arapho", "2016", "Rachel King"));
        lovak.Add(new Lo("Vauban", "2017", "Blake Shinn"));
        lovak.Add(new Lo("Chevalier Rose", "2017", "Damian Lane"));
        lovak.Add(new Lo("Prasage Nocturne", "2019", "Stephane Pasquier"));
        lovak.Add(new Lo("Meydaan", "2020", "James McDonald"));
        lovak.Add(new Lo("Absurde", "2017", "Kerrin McEvoy"));
        lovak.Add(new Lo("Flatten The Curve", "2018", "Thore Hammer Hansen"));

        foreach (var lo in lovak)
        {
            Console.WriteLine(lo);
        }
    }
    
    
    public static void Play(Player player)
    {
        LovakKiirasa();
    }
}