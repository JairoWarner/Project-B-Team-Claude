
public static class Print
{
    private const string Red =          "\u001b[38;2;139;0;0m";
    private const string Gold =         "\u001b[38;2;255;215;0m";
    private const string Ivory =        "\u001b[38;2;245;240;230m";
    private const string Green =        "\u001b[38;2;34;139;34m";
    private const string Background =   "\u001b[48;2;26;26;26m";

    // Reset
    private const string Reset =        "\u001b[0m";

    public static void Setup()
    {
        Console.Write(Background);
        Console.Clear();
    }

    public static void Title(string text)
    {
        Console.WriteLine(Gold +"==============================" + Reset);
        Console.WriteLine(Red +  text + Reset);
        Console.WriteLine(Gold +"==============================" + Reset);
    }

    public static void Normal(string text)
    {
        Console.WriteLine(Ivory + text + Reset);
    }

    public static void Price(string text)
    {
        Console.WriteLine(Gold + text + Reset);
    }

    public static void Success(string text)
    {
        Console.WriteLine(Green + text + Reset);
    }

    public static void Error(string text)
    {
        Console.WriteLine(Red + text + Reset);
    }
}
