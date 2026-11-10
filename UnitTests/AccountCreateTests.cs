namespace UnitTests;

[TestClass]
public class AccountCreateTests
{
    [DataTestMethod]
    public void TestPasswordHash()
    {
        // arrange
        string password = "scoobySnacks@";
        
        // act
        string passHash = AccountsLogic.HashPassword(password);
        
        // assert
        Assert.AreNotEqual(password, passHash);
        Assert.IsTrue(AccountsLogic.VerifyPassword(password, passHash));
    }

    public void PassWordMatchTest()
    {
        // arrange
        string password1 = "scoobySnacks@";
        string password2 = "scoobySnacks@";
        
        // act
        AccountsLogic.HashPassword(password1);
    }
}