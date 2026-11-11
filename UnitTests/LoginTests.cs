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
        AccountModel? result = AccountsLogic.CheckLogin(m, p);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(m, result.EmailAddress);
    }

    [DataTestMethod]
    [DataRow("User@example.com", "Wrong123!")]     // S3
    [DataRow("unknown@example.com", "Test1234!")]  // S4
    public void LoginInvalidCredentials(string m, string p)
    {
        // arrange

        // act
        AccountModel? result = AccountsLogic.CheckLogin(m, p);

        // assert
        Assert.IsNull(result);
    }

    [DataTestMethod]
    [DataRow("User@example.com", "Wrong123!", false)]
    [DataRow("User@example.com", "Wrong123!", false)]
    [DataRow("User@example.com", "Test1234!", true)]
    public void LoginAttemptsTest(string m, string p, bool expected)
    {
        // arrange
        
        // act
        AccountModel? result = AccountsLogic.CheckLogin(m, p);

        // assert
        if (expected)
            Assert.IsNotNull(result);
        
        if (!expected)
            Assert.IsNull(result);
    }

   
}
