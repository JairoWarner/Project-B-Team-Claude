using System;
using System.Linq;
using System.Globalization;
//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }
    private static AccountAccess _access = new();

    public AccountsLogic()
    {
        // Could do something here

    }

    public AccountModel CheckLogin(string email, string password)
    {


        AccountModel acc = _access.GetByEmail(email);
        if (acc != null && acc.Password == password)
        {
            CurrentAccount = acc;
            return acc;
        }
        return null;
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
            email,password,phonenumber,"Customer",DateofBirth);
        _access.Write(account);
    }
}

