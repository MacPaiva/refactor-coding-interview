using FluentAssertions;
using LegacyApp.Model;
using LegacyApp.Service;
using NSubstitute;
using Xunit;

namespace LegacyApp.Tests.Unit.Service;

public class UserServiceTests
{
    private readonly IUserService _sut;
    private readonly IClientService _clientService = Substitute.For<IClientService>();
    private readonly IUserCreditCheckService _userCreditCheckService = Substitute.For<IUserCreditCheckService>();
    private readonly IUserDataAccessService _userDataAccessService = Substitute.For<IUserDataAccessService>();

    public UserServiceTests()
    {
        _sut = new UserService(_clientService, _userCreditCheckService, _userDataAccessService);
    }
    
    // User should be added when all info is OK.
    [Fact]
    private void AddUser_ShouldAdd_WhenUserInfoIsOk()
    {
        // Arrange
        const string firName = "Lucas";
        const string surName = "Paiva";
        var birthDate = DateTime.Parse("1992-01-01");
        const string email = "lucaspaiva@email.com";

        var client = new Client
        {
            Id = 1324,
            Name = firName + " " + surName,
            ClientStatusEnum = ClientStatusEnum.Gold
        };

        _clientService.GetClientById(Arg.Is(client.Id)).Returns(client);
        _userCreditCheckService.CreditCheck(Arg.Any<User>()).Returns(true);
        _userDataAccessService.AddUser(Arg.Any<User>());

        // Act
        var result = _sut.AddUser(firName, surName, email, birthDate, client.Id);

        // Assert
        result.Should().BeTrue();
        _clientService.Received(1).GetClientById(Arg.Is(client.Id));
        _userCreditCheckService.Received(1).CreditCheck(Arg.Any<User>());
        _userDataAccessService.Received(1).AddUser(Arg.Any<User>());
    }
    
    // User should not be added when firname, surname or email are invalid
    [Xunit.Theory]
    [InlineData(null, "Paiva", "lucaspaiva@email.com")]
    [InlineData("Lucas", null, "lucaspaiva@email.com")]
    [InlineData("Lucas", "Paiva", "lucaspaivaemailcom")]
    public void AddUser_ShoutNotAdd_WhenUserNameOrEmailIsNotValid(string? firName, string? surName, string? email)
    {
        // Arrange
        var birthDate = DateTime.Parse("1992-01-01");

        var client = new Client
        {
            Id = 1324,
            Name = firName + " " + surName,
            ClientStatusEnum = ClientStatusEnum.Gold
        };

        // Act
        var result = _sut.AddUser(firName, surName, email, birthDate, client.Id);

        // Assert
        result.Should().BeFalse();
        _clientService.Received(0).GetClientById(Arg.Is(client.Id));
        _userCreditCheckService.Received(0).CreditCheck(Arg.Any<User>());
        _userDataAccessService.Received(0).AddUser(Arg.Any<User>());
    }
    
    // User should not be added when age is less or equal to 21
    [Fact]
    public void AddUser_ShouldNotAdd_WhenUserAgeIsLessOrEqualTo21()
    {
        // Arrange
        const string firName = "Lucas";
        const string surName = "Paiva";
        var birthDate = DateTime.Parse("2004-01-01");
        const string email = "lucaspaiva@email.com";
        const int id = 1234;
        
        // Act
        var result = _sut.AddUser(firName, surName, email, birthDate, id);

        // Assert
        result.Should().BeFalse();
        _clientService.Received(0).GetClientById(Arg.Any<int>());
        _userCreditCheckService.Received(0).CreditCheck(Arg.Any<User>());
        _userDataAccessService.Received(0).AddUser(Arg.Any<User>());
    }
    
    // User should NOT be added when credit check fails
    [Fact]
    public void AddUser_ShouldNotAdd_WhenCreditCheckFails()
    {
        // Arrange
        const string firName = "Lucas";
        const string surName = "Paiva";
        var birthDate = DateTime.Parse("1992-01-01");
        const string email = "lucaspaiva@email.com";
        
        var client = new Client
        {
            Id = 1324,
            Name = firName + " " + surName,
            ClientStatusEnum = ClientStatusEnum.Gold
        };

        _clientService.GetClientById(Arg.Is(client.Id)).Returns(client);
        _userCreditCheckService.CreditCheck(Arg.Any<User>()).Returns(false);
        
        // Act
        var result = _sut.AddUser(firName, surName, email, birthDate, client.Id);

        // Assert
        result.Should().BeFalse();
        _clientService.Received(1).GetClientById(Arg.Is(client.Id));
        _userCreditCheckService.Received(1).CreditCheck(Arg.Any<User>());
        _userDataAccessService.Received(0).AddUser(Arg.Any<User>());
        
    }
}