using System.Runtime.InteropServices;

public class CustomerMenu
{
    public static void Start()
    {
        Style.Title("Welcome/欢迎");
        string menuChoice = "";
        while (menuChoice != "7")
        {
            Style.Setup();
            Style.Title("Homepage");
            Style.Normal("1: My Details:");
            Style.Normal("2: Make Reservation:");
            Style.Normal("3: Show Reservations:");
            Style.Normal("4: Restaurant Layout:");
            Style.Normal("5: Food menu");
            Style.Normal("6: About Us");
            Style.Normal("7: Log out");
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
                    Console.WriteLine("Not implemented yet!");
                    break;

                case "4":
                    Console.WriteLine("Not implemented yet!");
                    break;

                case "5":
                    FoodMenu menu = new();
                    menu.ChooseMenu();
                    break;

                case "6":
                    Console.WriteLine("Not implemented yet!");
                    break;

                case "7":
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