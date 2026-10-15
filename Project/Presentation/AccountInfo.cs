namespace Project.Presentation;

public class AccountInfo
{ 
    public void PersonalMenu()
    {
        Style.Title("Personal Menu");
        Console.WriteLine("1: Personal info");
        Console.WriteLine("2: Active reservations");
        Console.WriteLine("3: Past reservations");
        Console.WriteLine("4: Past bills");
        string choice = Console.ReadLine();

        if (choice == "1")
            ShowAccountInfo(AccountsLogic.CurrentAccount);
        
        // if (choice == "2")
            
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
}