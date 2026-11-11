using Project.Presentation;

public class AdminMenu
{
    public static void Start()
    {
        string menuChoice = "";
        while (menuChoice != "8")
        {
            Style.Setup();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════╗");
            Console.WriteLine("║      ADMIN       ║");
            Console.WriteLine("║   CONTROL PANEL  ║");
            Console.WriteLine("╚══════════════════╝");
            Console.ResetColor();
            Console.WriteLine("Welcome to the Admin Homepage");
            Style.Title("Admin Homepage");
            Style.Normal("1: My Details");
            Style.Normal("2: Make Reservation");
            Style.Normal("3: Show Reservations");
            Style.Normal("4: Restaurant Layout");
            Style.Normal("5: Food menu");
            Style.Normal("6: About Us");
            Style.Normal($"7: Manage Reservations {Style.Rood}(Under Construction){Style.Reset}");
            Style.Normal("8: Log out");
            menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    AccountInfo accountInfo = new();
                    accountInfo.PersonalMenu();
                    break;

                case "2":
                    AddReservation addReservation = new();
                    addReservation.start();
                    break;

                case "3":
                    ShowReservation showReservations = new();
                    showReservations.ShowReservations();
                    break;

                case "4":
                    RestaurantMap.Start();
                    break;

                case "5":
                    FoodMenu menu = new();
                    menu.ChooseMenu();
                    break;

                case "6":
                    RestaurantInfo.PrintInfo();
                    break;

                case "7":
                    Style.Normal("Manage Reservations is under construction.");
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;

                case "8":
                    Console.Clear();
                    Console.WriteLine("You have been logged out");
                    UserLogin.LoginStart();
                    break;

                default:
                    Style.Error("That option isn't available");
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
