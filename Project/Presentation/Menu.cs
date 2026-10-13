static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Print.Normal("Enter 1 to login");
        Print.Normal("Enter 2 to create account");
        Print.Normal("Enter 3 to do something else in the future");

        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            AccountCreate.Create();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}