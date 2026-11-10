namespace Project.Presentation;

public class AccountInfo
{ 
    public void PersonalMenu()
    {
        Style.Title("Personal Menu");
        Console.WriteLine("1: Personal info");
        Console.WriteLine("2: Change personal info");
        Console.WriteLine($"3: Past reservations {Style.Rood}(Under Construction){Style.Reset}");
        Console.WriteLine($"4: Past bills {Style.Rood}(Under Construction){Style.Reset}");
        string choice = Console.ReadLine();

        if (choice == "1")
            ShowAccountInfo(AccountsLogic.CurrentAccount);
        
        if (choice == "2")
            ChangeInfo();
    }
    
    public void ShowAccountInfo(AccountModel account)
    {
        if (account == null)
        {
            Console.WriteLine("Account not found");
            return;
        }
        
        Console.Clear();
        Console.WriteLine($"Name: {account.FullName}");
        Console.WriteLine($"Date of birth: {account.DateOfBirth}");
        Console.WriteLine($"Email: {account.EmailAddress}");
        Console.WriteLine($"Phone number: {account.Phone}");
        Console.WriteLine($"\nPress Enter to continue");
        Console.ReadLine();
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