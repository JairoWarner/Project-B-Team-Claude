using System;
using System.Linq;
using System.Globalization;
public static class AccountCreate
{   
    public static String FirstName = "";
    public static String LastName = "";
    public static String DateofBirth;
    public static DateTime Birthdate;
    public static String email = "";
    public static String Phonenumber = "";
    public static String password = "";
//====================================================================
    public static void Start()
    {
        Console.Clear();
        
        Style.Title("1: Login");
        Style.Title("2: Register");
        string choice = Console.ReadLine();
        
        if (choice == "1")
            UserLogin.LoginStart();
        
        Firstname();
    }
    
//====================================================================
    public static void Firstname()
    {
        Style.Logo();
        Style.Title("Please enter your First Name");
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
        Style.Logo();
        Style.SuccesHeader();
        Style.Success($"{FirstName} is correct toegevoegd!");
        Lastname();
    }
//===================================================================
    public static void Lastname()
    {
        Style.Title("Please enter your lastname"); 
        while (true)
        {
            LastName = Console.ReadLine();  
            if (!AccountsLogic.ValidateLastName(LastName))
            {
                ShowLastNameError();    
                continue;
            }
            break;
        }
        Style.Logo();
        Style.SuccesHeader();
        Style.Success($"{LastName} is correct toegevoegd!");
        Dateofbirth();
    }
//================================================================
    public static void Dateofbirth()
    {
        Style.Title("Please enter your Date of Birth as: (dd/MM/yyyy)");
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
        Style.Logo();
        Style.SuccesHeader();
        Style.Success($"{DateofBirth} is correct toegevoegd!");
        Emailaddress();
}
//===================================================================
    public static void Emailaddress()
    {
        Style.Title("Please enter your email Adrres");
        while (true)
        {
            email = Console.ReadLine() ?? "";
            if (!AccountsLogic.CheckEmail(email))
            {
                ShowEmailError();
                continue;
            }
            if (AccountsLogic.EmailExists(email))
            {
                Style.ErrorHeader();
                Style.Error("an account with this email already exists");
                continue;
            }
            break;
        }
        Style.Logo();
        Style.SuccesHeader();
        Style.Success($"{email} is correct toegevoegd!");
        PhoneNumber();
    }
//===================================================================
    public static void PhoneNumber()
    {
        Style.Title("Please enter your phone number");
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
        Style.Logo();
        Style.SuccesHeader();
        Style.Success($"{Phonenumber} is correct toegevoegd!");
        Password();
    }
//===================================================================
    public static void Password()
    {
        Style.Title("Please enter your password");
        while (true)
        {
            password = AccountsLogic.PasswordTransform();
            if (!AccountsLogic.ValidatePassword(password))
            {
                ShowPasswordError();
                continue;
            }
            break;
        }
        Style.Logo();
        Style.SuccesHeader();
        Style.Success("Password is correct toegevoegd!");
        AccountsLogic.CreateAccount(FirstName, LastName, email, password, Phonenumber, DateofBirth);
        UserLogin.LoginStart();
    }
//===================================================================
    private static void ShowFirstNameError()
    {
        Style.Logo();
        Style.ErrorHeader();
        Style.Error("First name is Required");
        Style.Error("Minimum length of First name is 2 caracters");
        Style.Error("Maximum length of First name is 16 caracters");
        Style.Error("username should only contain letters");
        Style.Title("Please enter your First Name");
    }
    private static void ShowLastNameError()
    {
        Style.Logo();
        Style.ErrorHeader();
        Style.Error("Last name is Required");
        Style.Error("Minimum length of Last name is 2 caracters");
        Style.Error("Maximum length of Last name is 16 caracters");
        Style.Error("username should only contain letters");   
        Style.Title("Please enter your Last Name");                 
    }
    private static void ShowDateError()
    {
        Style.Logo();
        Style.ErrorHeader();
        Style.Error("Date is Required");
        Style.Error("Format should be formatted dd/MM/yyyy");
        Style.Error("Birth date should be valid");
        Style.Error("Age should be between 16 and 130 years old");
        Style.Title("Please enter your Date of Birth as: (dd/MM/yyyy)");
    }
    private static void ShowEmailError()
    {
        Style.Logo();
        Style.ErrorHeader();
        Style.Error("Email is required");
        Style.Error("email address should contain at least an @ and a . ");
        Style.Error("Email address needs at least 3 characters before @");
        Style.Error("Email address cannot contain spaces");
        Style.Title("Please enter your email address");

    }
    private static void ShowPhoneNumberError()
    {
        Style.Logo();
        Style.ErrorHeader();
        Style.Error("Phone number is required");
        Style.Error("Phone number must only contain digits, +, - and spaces are allowed");
        Style.Error("Phone number Must contain 8 to 15 digits");
        Style.Title("Please enter your phone number");
    }
    private static void ShowPasswordError()
    {
        Style.Logo();
        Style.ErrorHeader();
        Style.Error("Password is required");
        Style.Error("Password needs to be between 8 and 16 characters");
        Style.Error("Password needs at least one special character");
        Style.Error("Password needs at least one uppercase letter");
        Style.Title("Please enter your password");
    }
}
