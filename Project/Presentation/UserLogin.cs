static class UserLogin
{
    public static void LoginStart()
    {
        while (true)
        {
            Style.Title("Welcome to the login page");
            
            Style.Normal("1: to create an account");
            Style.Normal("2: to Log in");
            Style.Normal("3: to exit the program");
            string choice = Console.ReadLine();

            if  (choice == "1")
                AccountCreate.Start();
            
            if (choice == "3")
            {
                Environment.Exit(0);
            }

            Style.Normal($"Please enter your {Style.Groen}email{Style.Reset} address:");
            string email = Console.ReadLine();

            if (!AccountsLogic.CheckEmail(email))
            {
                continue;
            }

            while (true)
            {
                Console.Clear();
                Style.Title("Welcome to the login page\n");
                Style.Normal($"Please enter your {Style.Groen}password{Style.Reset}:");
                
                string password = AccountsLogic.PasswordTransform();

                if (!AccountsLogic.CheckPassword(password))
                {
                    continue;
                }

                AccountModel? acc = AccountsLogic.CheckLogin(email, password);

                if (acc != null)
                {
                    Console.Clear();
                    Style.SuccesHeader();
                    
                    Style.Success($"\nWelcome back {acc.FullName}");
                    Style.Normal($"Your email is {Style.Groen}{acc.EmailAddress}{Style.Reset}");
                    Style.Normal($"\nPress {Style.Groen}ENTER{Style.Reset} to open the menu");
                    Console.ReadLine();
                    
                    if (acc.Role == "Customer")
                        CustomerMenu.Start();
                    
                    if (acc.Role == "Admin")
                        Console.WriteLine("No admin menu yet. Womp Womp");
                    
                    return;
                }

                Style.Error("Incorrect password");
            }
        }
    }
}