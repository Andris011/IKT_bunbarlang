namespace IKT_bunbarlang.Loverseny;

public class Lo
{
    private string _name;
    private string _age;
    private string _rider;

    public Lo(string name, string age, string rider)
    {
        _name = name;
        _age = age;
        _rider = rider;
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

    override public string ToString()
    {
        return $"Név: {_name}, kor: {_age}, zsoké: {_rider}";
    }
}