static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    
    public static void LoginStart()
    {
        while (true)
        {
            Style.Title("Welcome to the login page");
            
            Style.Normal("Please enter your email address:");
            string email = Console.ReadLine();
            
            accountsLogic.CheckEmail(email);
            
            Style.Normal("Please enter your password:");
            string password = accountsLogic.PasswordTransform();
            accountsLogic.CheckPassword(password);
            
            AccountModel acc = accountsLogic.CheckLogin(email, password);
            if (acc != null)
            {
                Style.Success("Welcome back " + acc.FullName);
                Style.Normal("Your email number is " + acc.EmailAddress);
            }
            else
            {
                Style.Error("No account found with that email and password");
            }
        }
    }
}