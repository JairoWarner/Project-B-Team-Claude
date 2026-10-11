namespace Project.Presentation;

public class UserInfo
{
    public void ShowAccountInfo(AccountModel account)
    {
        Console.WriteLine(account.FullName);
        Console.WriteLine(account.EmailAddress);
        Console.WriteLine(account.Phone);
        Console.WriteLine(account.FullName);
        Console.WriteLine(account.FullName);
    }
}