static class UserLogin
{
    public static void LoginStart()
    {
        while (true)
        {
            Style.Title("Welcome to the login page");
            
            Style.Normal("Please enter your email address:");
            string email = Console.ReadLine();

            if (!AccountsLogic.CheckEmail(email))
            {
                continue;
            }
            
            Style.Normal("Please enter your password:");
            string password = AccountsLogic.PasswordTransform();
            
            if (!AccountsLogic.CheckPassword(password))
            {
                continue;
            }
            
            AccountModel acc = AccountsLogic.CheckLogin(email, password);
            if (acc != null)
            {
                Style.Success("Welcome back " + acc.FullName);
                Style.Normal("Your email number is " + acc.EmailAddress);
                break;
            }

            if (acc == null)
            {
                Style.Error("No account found");
            }
        }
        
        CustomerMenu.Start();
    }
}