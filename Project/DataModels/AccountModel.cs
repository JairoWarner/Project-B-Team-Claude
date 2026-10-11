public class AccountModel
{

    public Int64 Id { get; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public string DateOfBirth { get; set; }

    public AccountModel(Int64 id, string email, string password, string fullname)
    {
        Id = id;
        EmailAddress = email;
        Password = password;
        FullName = fullname;
    }

    public AccountModel(long id, string emailAddress, string password, string fullName, string phone, string role, string dateOfBirth)
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



