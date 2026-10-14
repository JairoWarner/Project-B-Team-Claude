static class UserLogin
{
    public static void LoginStart()
    {
        while (true)
        {
            Style.Title("Welcome to the login page");
            
            Style.Normal("1: to create an account");
            Style.Normal("2: to Log in");
            string choice = Console.ReadLine();
            
            if  (choice == "1")
                AccountCreate.Start();

            Style.Normal("Please enter your email address:");
            string email = Console.ReadLine();

            if (!AccountsLogic.CheckEmail(email))
            {
                continue;
            }

            while (true)
            {
                Style.Normal("Please enter your password:");
                string password = AccountsLogic.PasswordTransform();

                if (!AccountsLogic.CheckPassword(password))
                {
                    continue;
                }

                AccountModel? acc = AccountsLogic.CheckLogin(email, password);

                if (acc != null)
                {
                    Style.Success("Welcome back " + acc.FullName);
                    Style.Normal("Your email is " + acc.EmailAddress);

                    CustomerMenu.Start();
                    return;
                }

                Style.Error("Incorrect password");
            }
        }
    }
}