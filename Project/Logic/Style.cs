using System.Dynamic;

public static class Print
{
    public const string Rood =             "\u001b[38;2;139;0;0m";
    private const string ErrorRood =         "\u001b[38;2;255;85;85m";   
    public const string Goud  =             "\u001b[38;2;255;215;0m";
    private const string Wit =              "\u001b[38;2;245;240;230m";
    private const string Groen =            "\u001b[38;2;34;139;34m";
    private const string SuccessGroen =     "\u001b[38;2;105;230;160m";
    private const string Achtergrond =      "\u001b[48;2;26;26;26m";
    private const string ErrorAchtergrond =  "\u001b[48;2;150;35;35m";
    private const string SuccessAchtergrond = "\u001b[48;2;16;61;39m";
    private const string LogoAchtergrond = "\u001b[48;2;100;0;0m";
    private const string Bold =             "\u001b[1m";
    public const string Reset =            "\u001b[0m";

    public static void Setup()
    {
        Console.Write(Achtergrond);
        Console.Clear();
    }

    public static void Logo()
    {
        Console.Clear();
        Console.WriteLine(Goud + Bold + LogoAchtergrond);
        Console.WriteLine(             @"            /`·.¸             "        );
        Console.WriteLine(             @"           /¸...¸`:·          "        );
        Console.WriteLine(             @"       ¸.·´  ¸   `·.¸.·´)     "        );
        Console.WriteLine(             @"      : © ):´;      ¸  {      "        );
        Console.WriteLine(             @"       `·.¸ `·  ¸.·´\`·¸)     "        );
        Console.WriteLine(             @"           `\\´´\¸.·´         "        );
        Console.WriteLine(             @"                              " + Reset);
        Console.WriteLine(Goud +        "==============================" + Reset);
        Console.WriteLine(Bold + Goud + "       Imperial dragon        " + Reset);
        Console.WriteLine(Goud +        "==============================" + Reset);
    }
    public static void Title(string text)
    { 
        Console.WriteLine(Bold + Wit + text + Reset);
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
        Console.WriteLine(SuccessGroen + text + Reset);
    }
    public static void SuccesHeader()
    {
        Console.WriteLine(SuccessAchtergrond + Wit + Bold + "      [✓] S U C C E S S :     " + Reset);
    }
    public static void Error(string text)
    {
        Console.WriteLine(ErrorRood + text + Reset);
    }
        public static void ErrorHeader()
    {
        Console.WriteLine(ErrorAchtergrond + Wit + Bold + "       [!] E R R O R :        " + Reset);
    }
}
