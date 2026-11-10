namespace UnitTests;

[TestClass]
public sealed class Test1
{
    [DataTestMethod]
    [DataRow("User@example.com", "Test1234!")]
    public void LoginValidCredentials(string m, string p)
    {
        // arrange

        // act
        AccountModel result = AccountsLogic.CheckLogin(m, p);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(m, result.EmailAddress);
    }

    [DataTestMethod]
    [DataRow("kevin@kevin.nl", "wrong")] // wrong password
    [DataRow("wrong1", "kevin")] // wrong email
    [DataRow("wrong2", "wrong")] // everything wrong
    [DataRow("", "")]
    [DataRow(null, null)]
    public void LoginInvalidCredentials(string m, string p)
    {
        // arrange
        string email = "User@example.com";
        string password = "Test1234!";
        
        // act 
        AccountModel result = AccountsLogic.CheckLogin(m, p);

        // assert
        Assert.IsNull(result);
    }
}
