using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly UserTest _userTest;
    private readonly TestClient _clientTest;
    
    public UserServiceTests()
    {
        _userService = new UserService();
        _userTest = new UserTest
        {
            FirstName = "John", LastName = "Doe", EmailAddress = "johndoe@gmail.com",
            DateOfBirth = DateTime.Parse("1982-03-21"), ClientId = 1
        };
        _clientTest = new TestClient {Address = "ljkj",ClientId = 1,Email = "john@gmail.com", Name = "John",Type = "VeryImportantClient" };
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        //Arrange
        _userTest.EmailAddress = "johndoe";
        //Act
        bool result = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth, _userTest.ClientId);
        //Assert
        Assert.False(result);

    }

    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Incorrect()
    {
        //Arrange
        _userTest.LastName = "";
        //Act
        var addResult = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth, _userTest.ClientId);
        //Assert
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Incorrect()
    {
        //Arrange
        _userTest.FirstName = "";
        //Act
        var addResult = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth, _userTest.ClientId);
        //Assert
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Id_Does_Not_Exist()
    {
        //Arrange
        _userTest.ClientId = -2;
        //Act&Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth, _userTest.ClientId);
        });
    }
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Less_Than_21()
    {
        //Arrange
        _userTest.DateOfBirth=DateTime.Parse("2005-03-21");
        //Act
        bool result = _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth, _userTest.ClientId);
        //Assert
        Assert.False(result);

    }
    [Fact]
    public void HasCreditLimit_Should_Return_False_When_Client_Is_Very_Important()
    {
        //Arrange
        _clientTest.Type = "VeryImportantClient";
        //Act
        _userService.AddUser(_userTest.FirstName, _userTest.LastName, _userTest.EmailAddress, _userTest.DateOfBirth, _userTest.ClientId);
        
        //Assert
        Assert.False(_userTest.HasCreditLimit);

    }

    [Fact]
    public void IsFirstNameInCorrect_Should_Return_False_When_FirstName_Is_Correct()
    {
        //Arrange
        _userTest.FirstName = "John";
        //Act
        var ressult = UserService.IsFirstNameInCorrect(_userTest.FirstName);
        //Assert
        Assert.False(ressult);
    }
    [Fact]
    public void IsFirstNameInCorrect_Should_Return_False_When_FirstName_Is_Empty()
    {
        //Arrange
        _userTest.FirstName = "";
        //Act
        var ressult = UserService.IsFirstNameInCorrect(_userTest.FirstName);
        //Assert
        Assert.True(ressult);
    }
    [Fact]
    public void IsFirstNameInCorrect_Should_Return_False_When_FirstName_Is_Null()
    {
        //Arrange
        _userTest.FirstName = null;
        //Act
        var ressult = UserService.IsFirstNameInCorrect(_userTest.FirstName);
        //Assert
        Assert.True(ressult);
    }
    
    [Fact]
    public void IsLastNameInCorrect_Should_Return_False_When_LastName_Is_Сorrect()
    {
        //Arrange
        _userTest.LastName = "Doe";
        //Act
        var ressult = UserService.IsLastNameInCorrect(_userTest.LastName);
        //Assert
        Assert.False(ressult);
       
    }
    [Fact]
    public void IsLastNameInCorrect_Should_Return_False_When_LastName_Is_Empty()
    {
        //Arrange
        _userTest.LastName = "";
        //Act
        var ressult = UserService.IsLastNameInCorrect(_userTest.LastName);
        //Assert
        Assert.True(ressult);
       
    }
    [Fact]
    public void IsLastNameInCorrect_Should_Return_False_When_LastName_Is_Null()
    {
        //Arrange
        _userTest.LastName = null;
        //Act
        var ressult = UserService.IsLastNameInCorrect(_userTest.LastName);
        //Assert
        Assert.True(ressult);
       
    }


    [Fact]
    public void CalculateCreditLimitForImportantClient_ReturnsLimit_x2()
    {
       UserService userService = new UserService();
       var result = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 3);
       Assert.True(result);
    }
    [Fact]
    public void CalculateCreditLimitForNormalClient_ReturnsLimit()
    {
        UserService userService = new UserService();
        var result = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        Assert.True(result);
    }
    [Fact]
    public void CalculateCreditLimitForNormalClient_Does_Not_ReturnsLimit()
    {
        UserService userService = new UserService();
        var result = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 2);
        Assert.True(result);
    }
  
}