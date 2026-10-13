using System;
using System.Linq;
public class AccountCreate
{   
    public String FirstName = "";
    public String LastName = "";

    public void Firstname()
    {
        Print.Logo();
        Print.Title("Please enter your First Name");
        while (true)
        {
            FirstName = Console.ReadLine();
            if (String.IsNullOrEmpty(FirstName) || 
                FirstName.Length <2 || 
                FirstName.Length >16 || 
                !FirstName.All(char.IsLetter))
                {
                Print.Logo();
                Print.ErrorHeader();
                Print.Error("First name is Required");
                Print.Error("Minimum length of First name is 2 caracters");
                Print.Error("Maximum length of First name is 16 caracters");
                Print.Error("username should only contain letters");
                Print.Title("Please enter your First Name");
                continue;
                }
                break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success("Firstname is correct toegevoegd!");
        Lastname();
    }

    public void Lastname()
    {
        Print.Title("Please enter your lastname"); 
        while (true)
        {
            LastName = Console.ReadLine();       
            if (String.IsNullOrEmpty(LastName) || 
                LastName.Length < 2 || 
                LastName.Length > 16 || 
                !LastName.All(char.IsLetter))
                {
                Print.Logo();
                Print.ErrorHeader();
                Print.Error("Last name is Required");
                Print.Error("Minimum length of Last name is 2 caracters");
                Print.Error("Maximum length of Last name is 16 caracters");
                Print.Error("username should only contain letters");
                continue;
                }
                break;
        }
        Print.Logo();
        Print.SuccesHeader();
        Print.Success("Lastname is correct toegevoegd!");
    }
//Print.Normal("Please enter your date of birth");
//Print.Normal("Please enter your email-adress");
//Print.Normal("Please enter your Phone Number");
//Print.Normal("Please enter your password");
//Print.Normal("");
}
