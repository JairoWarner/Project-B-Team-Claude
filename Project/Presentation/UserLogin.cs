static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Style.Title("Welcome to the login page");
        Style.Normal("Please enter your email address");
        string email = Console.ReadLine();
        Style.Normal("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Style.Success("Welcome back " + acc.FullName);
            Style.Normal("Your email number is " + acc.EmailAddress);

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Style.Error("No account found with that email and password");
        }
    }
}