using System;
using System.Linq;
using System.Globalization;
public class AccountCreate
{   
    public String FirstName = "";
    public String LastName = "";
    public String DateofBirth;
    public DateTime Birthdate;
    public String email = "";
    public String Phonenumber = "";
    public String password = "";
//====================================================================
    public void Firstname()
    {
        Print.Logo();
        Print.Title("Please enter your First Name");
        while (true)
        {
            FirstName = Console.ReadLine();
            if (!AccountsLogic.ValidateFirstName(FirstName))
            {
                ShowFirstNameError();
                continue;
            }
            break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success($"{FirstName} is correct toegevoegd!");
        Lastname();
    }
//===================================================================
    public void Lastname()
    {
        Print.Title("Please enter your lastname"); 
        while (true)
        {
            LastName = Console.ReadLine();  
            if (!AccountsLogic.ValidateLastName(FirstName))
            {
                ShowLastNameError();    
                continue;
            }
            break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success($"{LastName} is correct toegevoegd!");
        Dateofbirth();
    }
//================================================================
    public void Dateofbirth()
    {
        Print.Title("Please enter your Date of Birth as: (dd/MM/yyyy)");
        while (true)
        {
            DateofBirth = Console.ReadLine() ?? "";
            if (!AccountsLogic.ValidateDateofBirth(DateofBirth))
            {
                ShowDateError();
                continue;
            }
            Birthdate = DateTime.ParseExact(DateofBirth,"dd/MM/yyyy",CultureInfo.InvariantCulture);
            break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success($"{DateofBirth} is correct toegevoegd!");
        Emailaddress();
}
//===================================================================
    public void Emailaddress()
    {
        Print.Title("Please enter your email Adrres");
        while (true)
        {
            email = Console.ReadLine() ?? "";
            if (!AccountsLogic.ValidateEmail(email))
            {
                ShowEmailError();
                continue;
            }
            if (AccountsLogic.EmailExists(email))
            {
                Print.ErrorHeader();
                Print.Error("an account with this email already exists");
                continue;
            }
            break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success($"{email} is correct toegevoegd!");
        PhoneNumber();
    }
//===================================================================
    public void PhoneNumber()
    {
        Print.Title("Please enter your phone number");
        while (true)
        {
            Phonenumber = Console.ReadLine() ?? "";
            if (!AccountsLogic.ValidatePhoneNumber(Phonenumber))
            {
                ShowPhoneNumberError();
                continue;
            }
            break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success($"{Phonenumber} is correct toegevoegd!");
        Password();
    }
//===================================================================
    public void Password()
    {
        Print.Title("Please enter your password");
        while (true)
        {
            password = Console.ReadLine() ?? "";
            if (!AccountsLogic.ValidatePassword(password))
            {
                ShowPasswordError();
                continue;
            }
            break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success("Password is correct toegevoegd!");
        AccountsLogic.CreateAccount(FirstName, LastName, email, password, Phonenumber, DateofBirth);
    }
//===================================================================
    private void ShowFirstNameError()
    {
        Print.Logo();
        Print.ErrorHeader();
        Print.Error("First name is Required");
        Print.Error("Minimum length of First name is 2 caracters");
        Print.Error("Maximum length of First name is 16 caracters");
        Print.Error("username should only contain letters");
        Print.Title("Please enter your First Name");
    }
    private void ShowLastNameError()
    {
        Print.Logo();
        Print.ErrorHeader();
        Print.Error("Last name is Required");
        Print.Error("Minimum length of Last name is 2 caracters");
        Print.Error("Maximum length of Last name is 16 caracters");
        Print.Error("username should only contain letters");   
        Print.Title("Please enter your Last Name");                 
    }
    private void ShowDateError()
    {
        Print.Logo();
        Print.ErrorHeader();
        Print.Error("Date is Required");
        Print.Error("Format should be formatted dd/MM/yyyy");
        Print.Error("Birth date should be valid");
        Print.Error("Age should be between 16 and 130 years old");
        Print.Title("Please enter your Date of Birth as: (dd/MM/yyyy)");
    }
    private void ShowEmailError()
    {
        Print.Logo();
        Print.ErrorHeader();
        Print.Error("Email is required");
        Print.Error("email address should contain at least an @ and a . ");
        Print.Error("Email address needs at least 3 characters before @");
        Print.Error("Email address cannot contain spaces");
        Print.Title("Please enter your email address");

    }
    private void ShowPhoneNumberError()
    {
        Print.Logo();
        Print.ErrorHeader();
        Print.Error("Phone number is required");
        Print.Error("Phone number must only contain digits, +, - and spaces are allowed");
        Print.Error("Phone number Must contain 8 to 15 digits");
        Print.Title("Please enter your phone number");
    }
    private void ShowPasswordError()
    {
        Print.Logo();
        Print.ErrorHeader();
        Print.Error("Password is required");
        Print.Error("Password needs to be between 8 and 16 characters");
        Print.Error("Password needs at least one special character");
        Print.Error("Password needs at least one uppercase letter");
        Print.Title("Please enter your password");
    }
}
