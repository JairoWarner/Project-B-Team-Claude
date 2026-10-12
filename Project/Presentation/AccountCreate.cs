using System;

class AccountCreate
{   
    public AccountModel Account = new();
    String FirstName;
    String Lastname;
    public Create(Accountmodel Account)
    {
        Print.Title("LOG-IN PAGE");

        while (true)
        {
            Console.Clear();
            Print.Normal("Please enter your First Name");
            FirstName = Console.ReadLine();
            if (FirstName.IsNullOrEmpty || FirstName.Length <2 || FirstName.Length >16 || !firstName.All(char.IsLetter))
                Print.Error("First name is Required");
                Print.Error("Minimum length of First name is 2 caracters");
                Print.Error("Maximum length of First name is 16 caracters");
                Print.Error("username should only contain letters");
            Print.Normal("Please enter your middel + lastname");     
            Lastname = Console.ReadLine();       
            if (Lastname.IsNullOrEmpty || Lastname.Length <2 || LastName.Length >16 || !LastName.All(char.IsLetter))
                Print.Error("Last name is Required");
                Print.Error("Minimum length of Last name is 2 caracters");
                Print.Error("Maximum length of Last name is 16 caracters");
                Print.Error("username should only contain letters");
            Print.Normal("Please enter your date of birth");
            
            if (Dateofbirth) 
            
            break;
        }


        Print.Normal("Please enter your date of birth");
        Print.Normal("Please enter your email-adress");
        Print.Normal("Please enter your Phone Number");
        Print.Normal("Please enter your password");
        Print.Normal("");
    }
}
