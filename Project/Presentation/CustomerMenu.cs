using System.Runtime.InteropServices;
using Project.Presentation;

public class CustomerMenu
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
            "7: Log out"
        ];
        
        while (true)
        {
            Style.Setup();
            Style.Logo();
            Style.Title("Homepage");
            
            int choice = ArrowNavigation.ReadArray(Menu);
            
            switch (choice)
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
                    RestaurantMap.Start();
                    break;

                case 5:
                    FoodMenu menu = new();
                    menu.ChooseMenu();
                    break;

                case 6:
                    RestaurantInfo.PrintInfo();
                    break;

                case 7:
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