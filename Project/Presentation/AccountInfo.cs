namespace Project.Presentation;

public class AccountInfo
{ 
    public void ShowAccountInfo(AccountModel account)
    {
        if (account == null)
        {
            Console.WriteLine("Account not found");
            return;
        }
        
        Console.WriteLine($"Name: {account.FullName}");
        Console.WriteLine($"Date of birth: {account.DateOfBirth}");
        Console.WriteLine($"Email: {account.EmailAddress}");
        Console.WriteLine($"Phone number: {account.Phone}");
    }
}