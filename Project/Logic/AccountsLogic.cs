using System;
using System.Linq;
using System.Globalization;
//This class is not static so later on we can use inheritance and interfaces
using System.ComponentModel;
using System.Text;
using Project.DataModels;
using System.Security.Cryptography;

public class AccountsLogic
{
    public static AccountModel? CurrentAccount { get; private set; }
    private static AccountAccess _access = new();

    public static bool CheckEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        int index1 = email.IndexOf("@");
        int index2 = email.IndexOf(".");

        if (!email.Contains("@") || !email.Contains(".") || index1 > index2 || email.Contains(" "))
        {
            return false;
        }

        string part = email.Split('@')[0];

        if (part.Length < 3)
        {
            return false;
        }

        return true;
    }

    public static bool CheckPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return false;
        }

        if (password.Length < 8)
        {
            return false;
        }

        return true;
    }

    public static AccountModel? CheckLogin(string email, string password)
    {
        AccountModel? acc = _access.GetByEmail(email);

        if (acc == null)
        {
            Style.Error("Email or password is wrong");
            return null;
        }

        if (!VerifyPassword(password, acc.Password))
        {
            Style.Error("Email or password is wrong");
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
               && FirstName.Length <= 16;
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
                "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime birthdate))
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


    public static void CreateAccount(string firstName, string lastName, string email, string password, string phonenumber, string DateofBirth)
    {
        string HashedPassword = HashPassword(password);
        
        var account = new AccountModel(0, $"{firstName} {lastName}", email, HashedPassword, phonenumber, CustomerRole.Customer, DateofBirth);
        _access.Write(account);
    }

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(32);

        return Convert.ToBase64String(salt) + "." +
               Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        if (string.IsNullOrEmpty(storedHash))
        {
            return false;
        }

        if (!storedHash.Contains('.'))
        {
            return string.Equals(password, storedHash, StringComparison.Ordinal);
        }

        var parts = storedHash.Split('.', 2);

        if (parts.Length != 2)
        {
            return false;
        }

        try
        {
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                100000,
                HashAlgorithmName.SHA256);

            byte[] testHash = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(hash, testHash);
        }
        catch (FormatException)
        {
            return string.Equals(password, storedHash, StringComparison.Ordinal);
        }
    }
    
    public static void UpdateInfo(string email, string phonenumber)
    {
        if (CurrentAccount == null)
            return;

        CurrentAccount.EmailAddress = email;
        CurrentAccount.Phone = phonenumber;

        _access.UpdateInfo(CurrentAccount);
    }
}