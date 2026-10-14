using System.Runtime.InteropServices;

public class Menu
{
    public static void Start()
    {
        string menuChoice = "";
        while (menuChoice != "7")
        {
            Print.Setup();
            Print.Title("Homepage");
            Print.Normal("1: My Details:");
            Print.Normal("2: Make Reservation:");
            Print.Normal("3: Show Reservations:");
            Print.Normal("4: Restaurant Layout:");
            Print.Normal("5: Food menu");
            Print.Normal("6: About Us");
            Print.Normal("7: Log out");
            menuChoice = Console.ReadLine();
            
            switch (menuChoice)
            {
                case "1":
                    break;

                case "2":
                    AddReservation addReservation = new();
                    addReservation.start();
                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    FoodMenu menu = new();
                    menu.ChooseMenu();
                    break;

                case "6":
                    break;

                case "7":
                    break;

                default:
                    Print.Error("That option isn't available");
                    Print.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;
            }
        }
    }
}