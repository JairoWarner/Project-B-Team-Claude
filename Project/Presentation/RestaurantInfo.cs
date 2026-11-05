namespace Project.Presentation;

public class RestaurantInfo
{
    public static void PrintInfo()
    {
        Console.Clear();

        Style.Normal($"{Style.Goud}══════════════════════════════════════{Style.Reset}");
        Style.Title($"{Style.LogoAchtergrond}{Style.Goud}        RESTAURANT INFORMATION        {Style.Reset}");
        Style.Normal($"{Style.Goud}══════════════════════════════════════{Style.Reset}");

        Style.Normal($"\n{Style.LightGrey}Welcome to Zhà Gǒu!");
        Style.Normal($"{Style.LightGrey}Here you can find our contact details.");

        Style.Title($"\n{Style.Goud}CONTACT INFORMATION");
        Style.Normal($"{Style.LightGrey}──────────────────────────────────────");
        Style.Normal($"Email       : {Style.Goud}ZhàGǒu@BingChillin.com");
        Style.Normal("Phone       : +31 10 123 4567");
        Style.Normal("Website     : www.ZhàGǒu.com");

        Style.Title($"\n{Style.Goud}LOCATION");
        Style.Normal($"{Style.LightGrey}──────────────────────────────────────");
        Style.Normal("Address     : Kruiskade 100");
        Style.Normal("City        : Rotterdam");
        Style.Normal("Postal Code : 3011 AG");

        Style.Title($"\n{Style.Goud}OPENING HOURS");
        Style.Normal($"{Style.LightGrey}──────────────────────────────────────");
        Style.Normal("Monday - Friday : 12:00 - 22:00");
        Style.Normal("Saturday        : 12:00 - 23:00");
        Style.Normal("Sunday          : 13:00 - 21:00");

        Style.Normal($"\n{Style.Goud}══════════════════════════════════════{Style.Reset}");
        Style.Normal(
            $"\n{Style.LightGrey}Press {Style.Goud}{Style.Bold}ENTER{Style.Reset}" +
            $"{Style.LightGrey} to return to the menu");

        Console.ReadLine();
    }
}