using System.Runtime.InteropServices;

public class Menu
{
    public static void Start()
    {
        string menuChoice = "";
        while (menuChoice != "7")
        {
            Console.WriteLine();
            Console.WriteLine("Homepage");
            Console.WriteLine("1: My Details:");
            Console.WriteLine("2: Make Reservation:");
            Console.WriteLine("3: Show Reservations:");
            Console.WriteLine("4: Restaurant Layout:");
            Console.WriteLine("5: Food menu");
            Console.WriteLine("6: About Us");
            Console.WriteLine("7: Log out");
            menuChoice = Console.ReadLine();
            
            switch (menuChoice)
            {
                case "1":
                    Console.WriteLine("Not implemented yet!");
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
                    Console.WriteLine("Not implemented yet!");
                    break;

                default:
                    Console.WriteLine("That option isn't available");
                    break;
            }
        }
    }
}