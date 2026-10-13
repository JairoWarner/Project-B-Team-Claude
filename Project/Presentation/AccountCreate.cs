using System;
using System.Linq;
using System.Globalization;
public class AccountCreate
{   
    public String FirstName = "";
    public String LastName = "";
    public String DateofBirth;
    public DateTime Birthdate;
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
}
//===================================================================
    // public void Emailaddress()
    // {
    //     return null;
    // }
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
    }

//Print.Normal("Please enter your email-adress");
//Print.Normal("Please enter your Phone Number");
//Print.Normal("Please enter your password");
//Print.Normal("");
}
