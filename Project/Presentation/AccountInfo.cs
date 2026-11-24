namespace Project.Presentation;

public class AccountInfo
{
    public void PersonalMenu()
    {
        string[] Menu =
        [
            "1: Personal info",
            $"2: Past reservations {Style.Rood}(Under Construction){Style.Reset}",
            $"3: Past bills {Style.Rood}(Under Construction){Style.Reset}",
            "Go back"
        ];

        int MenuChoice = 0;

        while (true)
        {
            MenuChoice = ArrowNavigation.PrintMenu(Menu);

            Style.Title("Personal Menu");

            switch (MenuChoice)
            {
                case 1:
                    ShowAccountInfo(AccountsLogic.CurrentAccount);
                    break;
                case 2:
                    Style.Error("Not implemented yet");
                    break;
                case 3:
                    Style.Error("Not implemented yet");
                    break;
                case 4:
                    Console.Clear();
                    CustomerMenu.Start();
                    break;
            }
        }
    }

    public void ShowAccountInfo(AccountModel account)
    {
        if (account == null)
        {
            Console.WriteLine("Account not found");
            return;
        }

        string[][] PersonalInfo = new string[5][];
        
        PersonalInfo[0] = new string[] {$"Name: {account.FullName}", "Edit Name", "Back"};
        PersonalInfo[1] = new string[] {$"Date of birth: {account.DateOfBirth}", "Edit Date of birth", "Back"};
        PersonalInfo[2] = new string[] {$"Email: {account.EmailAddress}", "Edit Email", "Back"};
        PersonalInfo[3] = new string[] {$"Phone number: {account.Phone}", "Edit Phone Number", "Back"};
        PersonalInfo[4] = new string[] {"Go back"};

        (int, int) InfoChoice = (0, 0);
        
        while (true)
        {
            Console.Clear();
            
            InfoChoice = ArrowNavigation.PrintJaggedMenu(PersonalInfo, () =>
            {
                Style.Title($"\n{Style.Goud}YOUR INFO");
                Style.Normal($"{Style.LightGrey}──────────────────────────────────────\n");
            });

            switch (InfoChoice)
            {
                case (1, 1):
                    ChangeName();
                    break;
                case (2, 1):
                    ChangeDateOfBirth();
                    break;
                case (3, 1):
                    ChangeEmail();
                    break;
                case (4, 1):
                    ChangePhone();
                    break;
                case (5, 1):
                    PersonalMenu();
                    break;
            }
        }
    }

    public void ChangeEmail()
    {
        Console.Clear();

        string email = "";
        string phoneNumber = "";

        while (true)
        {
            Style.Normal("Enter new email address: ");
            email = Console.ReadLine();

            if (!AccountsLogic.CheckEmail(email))
            {
                Style.Error("Invalid email address");
                continue;
            }
            
            break;
        }

        AccountsLogic.UpdateEmail(email);
    }
    
    public void ChangeName()
    {
        Console.Clear();

        string name = "";

        while (true)
        {
            Style.Normal("Enter new name: ");
            name = Console.ReadLine();

            if (!AccountsLogic.ValidateFirstName(name))
            {
                Style.Error("Invalid name");
                continue;
            }

            break;
        }

        AccountsLogic.UpdateName(name);
    }
    
    public void ChangePhone()
    {
        Console.Clear();

        string phone = "";

        while (true)
        {
            Style.Normal("Enter new phone number: ");
            phone = Console.ReadLine();

            if (!AccountsLogic.ValidatePhoneNumber(phone))
            {
                Style.Error("Invalid number");
                continue;
            }

            break;
        }

        AccountsLogic.UpdatePhone(phone);
    }
    
    public void ChangeDateOfBirth()
    {
        Console.Clear();

        string dateOfBirth = "";

        while (true)
        {
            Style.Normal("Enter new date of birth: ");
            dateOfBirth = Console.ReadLine();

            if (!AccountsLogic.ValidateDateofBirth(dateOfBirth))
            {
                Style.Error("Invalid date Of Birth");
                continue;
            }

            break;
        }

        AccountsLogic.UpdateDateOfBirth(dateOfBirth);
    }
}