static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Print.Title("Welcome to the login page");
        Print.Normal("Please enter your email address");
        string email = Console.ReadLine();
        Print.Normal("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Print.Success("Welcome back " + acc.FullName);
            Print.Normal("Your email number is " + acc.EmailAddress);

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Print.Error("No account found with that email and password");
        }
    }
}