
public static class Con
{
    public static void Setup()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Clear();
    }

    public static void Title(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("==============================");
        Console.WriteLine($"  {text}");
        Console.WriteLine("==============================");
        Console.ResetColor();
    }

    public static void Success(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Error(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Price(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void Normal(string text)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
