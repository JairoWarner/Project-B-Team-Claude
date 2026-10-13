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
    private AccountAccess _access = new();

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
    // public static bool ValidateEmail()
    // {
        
    // }
    

}




