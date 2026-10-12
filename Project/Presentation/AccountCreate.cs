using System;
public AccountsModel Account = new();
class AccountCreate
{
    String FirstName;
    String Lastname;
    public Create(Accountsmodel Account)
    {
        Print.Title("LOG-IN PAGE");

        while (true)
        {
            Print.Normal("Please enter your First Name");
            FirstName = Console.ReadLine();
            if (FirstName.IsNullOrEmpty || FirstName.Length <2 || FirstName.Length >16 || !firstName.All(char.IsLetter))
                Print.Error("First name is Required");
                Print.Error("Minimum length of Firstname is 2 caracters");
                Print.Error("Maximum length of Firstname is 16 caracters");
                Print.Error("username should only contain letters");
            break;
        }

        Print.Normal("Please enter your middel + lastname");
        Print.Normal("Please enter your date of birth");
        Print.Normal("Please enter your email-adress");
        Print.Normal("Please enter your Phone Number");
        Print.Normal("Please enter your password");
        Print.Normal("");
    }
}
