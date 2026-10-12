public class AccountModel
{

    public Int64 Id { get; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public Int64? DateOfBirth { get; set; }

    public AccountModel(Int64 id, string fullname, string email, string password, string phone, string role, Int64? dateofbirth)
    {
        Id = id;
        FullName = fullname;
        EmailAddress = email;
        Password = password;
        Phone = phone;
        Role = role;
        DateOfBirth = dateofbirth;
    }

    public AccountModel(Int64 id, string emailAddress, string password, string fullName, string phone, string role, Int64 dateOfBirth)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        Phone = phone;
        Role = role;
        DateOfBirth = dateOfBirth;
    }
}



