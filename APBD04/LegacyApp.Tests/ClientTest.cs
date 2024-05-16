namespace LegacyApp.Tests;

public class ClientTest
{
    [Fact]
    public void GetName_Should_Return_Kowaliski()
    {
        // Arrange
        Client client = ClientRepository.Database[1];

        // Act
        string result = client.Name;

        // Assert
        Assert.Equal("Kowalski",result);
    }
    [Fact]
    public void GetClientId_Should_Return_1()
    {
        // Arrange
        Client client = ClientRepository.Database[1];

        // Act
        int result = client.ClientId;

        // Assert
        Assert.Equal(1,result);
    }
    [Fact]
    public void GetEmail_Should_Return_kowalski_małpa_kropka_pl()
    {
        // Arrange
        Client client = ClientRepository.Database[1];

        // Act
        string result = client.Email;

        // Assert
        Assert.Equal("kowalski@wp.pl",result);
    }
    [Fact]
    public void GetAdress_Should_Return_Warszawa_przecinek_spacja_Złota_12()
    {
        // Arrange
        Client client = ClientRepository.Database[1];

        // Act
        string result = client.Address;

        // Assert
        Assert.Equal("Warszawa, Złota 12",result);
    }
    [Fact]
    public void GetType_Should_Return_NormalClient()
    {
        // Arrange
        Client client = ClientRepository.Database[1];

        // Act
        string result = client.Type;

        // Assert
        Assert.Equal("NormalClient",result);
    }
}