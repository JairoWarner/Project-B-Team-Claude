using System.ComponentModel;
using System.Text;

public class AccountsLogic
{
    public static AccountModel? CurrentAccount { get; private set; }
    private AccountAccess _access = new();

    public bool CheckEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            Style.Error("Please fill in your email");
            return false;
        }
        
        int index1 = email.IndexOf("@");
        int index2 = email.IndexOf(".");

        if (!email.Contains("@") || !email.Contains(".") || index1 > index2 || email.Contains(" "))
        {
            Style.Error("invalid email format");
            return false;
        }
        
        string part = email.Split('@')[0];

        if (part.Length < 3)
        {
            Style.Error("Email is too short");
            return false;
        }

        return true;
    }

    public bool CheckPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            Style.Error("Please fill in your password");
            return false;
        }
        
        if (password.Length < 8)
        {
            Style.Error("Password has to be at least 8 characters long");
            return false;
        }
        
        return true;
    }

    public void CheckLogin(string email, string password)
    {
        
    }
    
    public string PasswordTransform()
    {
        var password = new StringBuilder();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password.Append(key.KeyChar);
                Console.Write('*');
            }
        }

        return password.ToString();
    }
}




