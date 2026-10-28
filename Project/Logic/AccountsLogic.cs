using System;
using System.Linq;
using System.Globalization;
//This class is not static so later on we can use inheritance and interfaces
using System.ComponentModel;
using System.Text;
using Project.DataModels;

public class AccountsLogic
{
    public static AccountModel? CurrentAccount { get; private set; }
    private static AccountAccess _access = new();

    public static bool CheckEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            Style.Error("Email is required");
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

    public static bool CheckPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            Style.Error("Password is required");
            return false;
        }
        
        if (password.Length < 8)
        {
            Style.Error("Password has to be at least 8 characters long");
            return false;
        }
        
        return true;
    }

    public static AccountModel? CheckLogin(string email, string password)
    {
        AccountModel acc = _access.GetByEmail(email);
        
        if (acc == null)
        {
            Style.Error("Email is invalid");
            return null;
        }
        
        if (acc.Password != password)
        {
            Style.Error("Password is invalid");
            return null;
        }

        CurrentAccount = acc;
        return acc;
    }
    
    public static string PasswordTransform()
    {
        string password = "";
        
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            
            if (key.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            
            if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write('*');
            }
        }

        return password.ToString();
    }
    public static bool ValidateFirstName(string FirstName)
    {
        return !string.IsNullOrEmpty(FirstName)
            && FirstName.Length >= 2
            && FirstName.Length <= 16
            && FirstName.All(char.IsLetter);
    }
    public static bool ValidateLastName(string LastName)
    {
        return !string.IsNullOrEmpty(LastName)
        && LastName.Length >= 2
        && LastName.Length <= 16
        && LastName.All(char.IsLetter);
    }
    public static bool ValidateDateofBirth(string DateofBirth)
    {
    if (!DateTime.TryParseExact(DateofBirth,
        "dd/MM/yyyy",CultureInfo.InvariantCulture,
        DateTimeStyles.None,out DateTime birthdate))
        {
            return false;
        }
        int age = DateTime.Today.Year - birthdate.Year;
        
        if (birthdate > DateTime.Today || age > 130 || age < 16)
        {
            return false;
        }
    return true;
    }   
    public static bool ValidateEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email)
            && !email.Contains(' ') && email.Contains('@')
            && email.Contains('.') && email.IndexOf('@') >= 3;
    }
    public static bool EmailExists(string email)
    {
        return _access.GetByEmail(email) != null;
    }

    public static bool ValidatePhoneNumber(string Phonenumber)
    {
        return !string.IsNullOrWhiteSpace(Phonenumber)
            && Phonenumber.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' ')
            && Phonenumber.Count(char.IsDigit) >= 8
            && Phonenumber.Count(char.IsDigit) <= 15;
    }
    public static bool ValidatePassword(string password)
    {
        return !string.IsNullOrEmpty(password)
            && password.Length >= 8 && password.Length <= 16
            && password.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
            && password.Any(char.IsUpper);
    }


    public static void CreateAccount(string firstName, string lastName, string email,
                                    string password, string phonenumber, string DateofBirth)
    {
        var account = new AccountModel(0 ,$"{firstName} {lastName}",
            email,password,phonenumber, CustomerRole.Customer, DateofBirth);
        _access.Write(account);
    }
}

