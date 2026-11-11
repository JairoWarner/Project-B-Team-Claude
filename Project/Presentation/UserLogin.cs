using Project.DataModels;
using Project.Presentation;


static class UserLogin
{
    public static void LoginStart()
    {
        string[] Menu =
        [
            "1: Login",
            "2: Register",
            "3: Go Back"
        ];

        while (true)
        {
            Console.Clear();
            Style.Title("Welcome to the login page");

            int choice = ArrowNavigation.PrintMenu(Menu);

            switch (choice)
            {
                case 1:
                    LoginPrint();
                    break;
                case 2:
                    AccountCreate.Start();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    public static void LoginPrint()
    {
        Console.Clear();
        Style.Title("Welcome to the login page\n");

        int Count = 0;

        while (true)
        {
            if (Count == 3)
            {
                Style.Error("To many try's your timed out");
                Thread.Sleep(30000);
                Count = 0;
                continue;
            }

            Style.Normal($"Please enter your {Style.Groen}email{Style.Reset} address:");
            string email = Console.ReadLine();

            Style.Normal($"Please enter your {Style.Groen}password{Style.Reset}:");
            string password = AccountsLogic.PasswordTransform();

            if (!AccountsLogic.CheckPassword(password))
            {
                Count += 1;
                Console.Clear();
                Style.Title("Welcome to the login page\n");
                Style.Error("Email or password is wrong");
                continue;
            }

            if (!AccountsLogic.CheckEmail(email))
            {
                Count++;
                Console.Clear();
                Style.Title("Welcome to the login page\n");
                Style.Error("Email or password is wrong");
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

                if (acc.Role == CustomerRole.Customer)
                    CustomerMenu.Start();
                else if (acc.Role == CustomerRole.Admin)
                    AdminMenu.Start();

                return;
            }

            Console.Clear();
            Style.Error("Email or password is wrong");
        }
    }
}