namespace Project.Presentation;

public class AccountInfo
{
    public void PersonalMenu()
    {
        string[] Menu =
        [
            "1: Personal info",
            "2: Change personal info",
            $"3: Past reservations {Style.Rood}(Under Construction){Style.Reset}",
            $"4: Past bills {Style.Rood}(Under Construction){Style.Reset}"
        ];

        int MenuChoice = 0;

        while (true)
        {
            MenuChoice = ArrowNavigation.ReadArray(Menu);

            Style.Title("Personal Menu");

            switch (MenuChoice)
            {
                case 1:
                    ShowAccountInfo(AccountsLogic.CurrentAccount);
                    break;
                case 2:
                    ChangeInfo();
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
        
        PersonalInfo[0] = new string[] {$"Name: {account.FullName}", "Edit Name"};
        PersonalInfo[0] = new string[] {$"Date of birth: {account.DateOfBirth}", "Edit Date of birth"};
        PersonalInfo[0] = new string[] {$"Email: {account.EmailAddress}", "Edit Email"};
        PersonalInfo[0] = new string[] {$"Phone number: {account.Phone}", "Edit Phone Number"};
        PersonalInfo[0] = new string[] {"Go back"};

        int InfoChoice = 0;
        
        while (true)
        {
            ArrowNavigation.ReadArray(PersonalInfo);

            switch (InfoChoice)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }

    public void ChangeInfo()
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

        while (true)
        {
            Style.Normal("Enter new phone number: ");
            phoneNumber = Console.ReadLine();

            if (!AccountsLogic.ValidatePhoneNumber(phoneNumber))
            {
                Style.Error("Invalid phone number");
                continue;
            }

            break;
        }

        AccountsLogic.UpdateInfo(email, phoneNumber);
    }
}