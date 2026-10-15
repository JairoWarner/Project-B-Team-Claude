public class AccountModel
{
    public Int64 Id { get; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public string DateOfBirth { get; set; }

    public AccountModel(Int64 id, string fullname, string email, string password, string phone, string role, string dateofbirth)
    {
        Id = id;
        FullName = fullname;
        EmailAddress = email;
        Password = password;
        Phone = phone;
        Role = role;
        DateOfBirth = dateofbirth;
    }
}



