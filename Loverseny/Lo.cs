namespace IKT_bunbarlang.Loverseny;

public class Lo
{
    private int _id;
    private string _name;
    private string _age;
    private string _rider;
    private double _szorzo;

    public Lo(int id, string name, string age, string rider)
    {
        _id = id;
        _name = name;
        _age = age;
        _rider = rider;
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Age
    {
        get => _age;
        set => _age = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Rider
    {
        get => _rider;
        set => _rider = value ?? throw new ArgumentNullException(nameof(value));
    }

    public double Szorzo => _szorzo;


    public void SzorzoHozzaadasa()
    {
        Random rnd = new Random();


        _szorzo = Math.Round(rnd.NextDouble() * (5.01-1.01) + 1.01, 2);

    }


    public void NyertesSzorzoHozzaadasa()
    {
        Random rnd = new Random();


        _szorzo = Math.Round(rnd.NextDouble() * (2.01-1.01) + 1.01, 2);
    }

    override public string ToString()
    {
        return $"{_id} | név: {_name}, születési év: {_age}, zsoké: {_rider}, szorzó: {_szorzo}";
        // return $"{_id, 2} | {_name,-20} | {_szorzo,5:F2} | {_rider}";
    }
}