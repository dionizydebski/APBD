namespace LegacyApp.Tests;

public class UserServiceTest
{
    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Empty()
    {
        // Arrange
        UserService service = new UserService();

        // Act
        bool result = service.AddUser("", "Kowalski", "adam@wp.pl", new DateTime(1980, 1, 1), 2);

        // Assert
        Assert.Equal(false,result);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Empty()
    {
        // Arrange
        UserService service = new UserService();

        // Act
        bool result = service.AddUser("Adam", "", "adam@wp.pl", new DateTime(1980, 1, 1), 2);

        // Assert
        Assert.Equal(false,result);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_And_LastName_Are_Empty()
    {
        // Arrange
        UserService service = new UserService();

        // Act
        bool result = service.AddUser("", "", "adam@wp.pl", new DateTime(1980, 1, 1), 1);

        // Assert
        Assert.Equal(false,result);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        // Arrange
        UserService service = new UserService();

        // Act
        bool result = service.AddUser("Adam", "Kowalski", "adam", new DateTime(1980, 1, 1), 2);

        // Assert
        Assert.Equal(false,result);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_Age_Less_Than_21()
    {
        // Arrange
        UserService service = new UserService();
    
        // Act
        bool result = service.AddUser("Adam", "Kowalski", "adam@wp.pl", DateTime.Now.AddYears(-21).AddMonths(1),2);
    
        // Assert
        Assert.Equal(false,result);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_CreditLimit_Too_Small()
    {
        // Arrange
        UserService service = new UserService();
    
        // Act
        bool result = service.AddUser("Adam", "Kowalski", "adam@wp.pl", new DateTime(1980, 1, 1),1);
    
        // Assert
        Assert.Equal(false,result);
    }
    [Fact]
    public void AddUser_Should_Return_True_When_ClientType_VeryImportantClient()
    {
        // Arrange
        UserService service = new UserService();
    
        // Act
        bool result = service.AddUser("Adam", "Kowalski", "adam@wp.pl", new DateTime(1980, 1, 1),2);
    
        // Assert
        Assert.Equal(true,result);
    }
    [Fact]
    public void AddUser_Should_Return_True_When_ClientType_ImportantClient()
    {
        // Arrange
        UserService service = new UserService();
    
        // Act
        bool result = service.AddUser("Adam", "Kowalski", "adam@wp.pl", new DateTime(1980, 1, 1),3);
    
        // Assert
        Assert.Equal(true,result);
    }
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Id_Doesnt_Exist_In_Database()
    {
        // Arrange
        UserService service = new UserService();
    
        // Act and Assert
        Assert.Throws<ArgumentException>(() => service.AddUser("Adam", "Kowalski", "adam@wp.pl", new DateTime(1980, 1, 1),7));
    }
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_LastName_Doesnt_Exist_In_Database()
    {
        // Arrange
        UserService service = new UserService();
    
        // Act and Assert
        Assert.Throws<ArgumentException>(() => service.AddUser("Adam", "Adamski", "adam@wp.pl", new DateTime(1980, 1, 1),3));
    }
}