using Project.Presentation;

public class AdminMenu
{
    public static void Start()
    {
        string[] Menu =
        [
            "1: My Details",
            "2: Make Reservation",
            "3: Show Reservations",
            "4: Restaurant Layout",
            "5: Food menu",
            "6: About Us",
            "7: Manage Reservations",
            "8: Manage Menus",
            "9: Log out"
        ];

        int menuChoice = 0;
        while (menuChoice != 8)
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
            menuChoice = ArrowNavigation.PrintMenu(Menu);
            
            switch (menuChoice)
            {
                case 1:
                    AccountInfo accountInfo = new();
                    accountInfo.PersonalMenu();
                    break;

                case 2:
                    AddReservation addReservation = new();
                    addReservation.start();
                    break;

                case 3:
                    ShowReservation showReservations = new();
                    showReservations.ShowReservations();
                    break;

                case 4:
                    RestaurantMap.Start(true);
                    break;

                case 5:
                    FoodMenu menu = new();
                    menu.ChooseMenu();
                    break;

                case 6:
                    RestaurantInfo.PrintInfo();
                    break;

                case 7:
                    ManageResMenu manageResMenu = new();
                    manageResMenu.ManageRes();
                    break;

                 case 8:
                    MenuManagement menuManagement = new();
                    menuManagement.Start();
                    break;

                case 9:
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
