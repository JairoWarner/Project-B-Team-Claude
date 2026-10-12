public static class Print
{
    private const string Rood =          "\u001b[38;2;139;0;0m";
    private const string Goud =         "\u001b[38;2;255;215;0m";
    private const string Wit =        "\u001b[38;2;245;240;230m";
    private const string Groen =        "\u001b[38;2;34;139;34m";
    private const string Achtergrond =   "\u001b[48;2;26;26;26m";
    private const string Reset =        "\u001b[0m";

    public static void Setup()
    {
        Console.Write(Achtergrond);
        Console.Clear();
    }
    public static void Title(string text)
    {
        Console.WriteLine(Goud +"==============================" + Reset);
        Console.WriteLine(Rood + "       " + text + Reset);
        Console.WriteLine(Goud +"==============================" + Reset);
    }
    public static void Normal(string text)
    {
        Console.WriteLine(Wit + text + Reset);
    }
    public static void Price(string text)
    {
        Console.WriteLine(Goud + text + Reset);
    }
    public static void Success(string text)
    {
        Console.WriteLine(Groen + text + Reset);
    }
    public static void Error(string text)
    {
        Console.WriteLine(Rood + text + Reset);
    }
}
