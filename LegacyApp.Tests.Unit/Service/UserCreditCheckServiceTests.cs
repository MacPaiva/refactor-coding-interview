using FluentAssertions;
using LegacyApp.Model;
using LegacyApp.Service;
using NSubstitute;
using Xunit;

namespace LegacyApp.Tests.Unit.Service;

public class UserCreditCheckServiceTests
{
    private readonly IUserCreditCheckService _sut;
    private readonly IUserCreditService _userCreditService = Substitute.For<IUserCreditService>();

    public UserCreditCheckServiceTests()
    {
        _sut = new UserCreditCheckService(_userCreditService);
    }

    
    
    // Credit Check should return TRUE skipping credit check when client name is VeryImportantClient
    [Fact]
    public void CreditCheck_ShouldReturnTrue_WhenClientNameIsVeryImportantClient()
    {
        // Arrange
        var client = new Client
        {
            Id = 1324,
            Name = "VeryImportantClient",
            ClientStatusEnum = ClientStatusEnum.Gold
        };

        var user = new User()
        {
            Client = client,
            DateOfBirth = DateTime.Parse("1992-01-01"),
            EmailAddress = "lucaspaiva@email.com",
            Firstname = "Lucas",
            Surname = "Paiva"
        };

        // Act
        var result = _sut.CreditCheck(user);

        // Assert
        _userCreditService.Received(0)
            .GetCreditLimit(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTime>());

        result.Should().BeTrue();
    }

    // Credit Check should return TRUE doing credit check and credit limit is 250 when client name is ImportantClient
    // Credit Check should return FALSE doing credit check and credit limit is 249 when client name is ImportantClient
    [Xunit.Theory]
    [InlineData(250, true)]
    [InlineData(249, false)]
    public void CreditCheck_ShouldDoCreditCheckAndDoubleCreditLimit_WhenClientNameIsImportantClient(
        int creditLimit, bool expectedResult)
    {
        // Arrange
        var client = new Client
        {
            Id = 1324,
            Name = "ImportantClient",
            ClientStatusEnum = ClientStatusEnum.Gold
        };

        var user = new User()
        {
            Client = client,
            DateOfBirth = DateTime.Parse("1992-01-01"),
            EmailAddress = "lucaspaiva@email.com",
            Firstname = "Lucas",
            Surname = "Paiva"
        };

        _userCreditService.GetCreditLimit(Arg.Is(user.Firstname), Arg.Is(user.Surname), Arg.Is(user.DateOfBirth))
            .Returns(creditLimit);

        // Act
        var result = _sut.CreditCheck(user);

        // Assert
        _userCreditService.Received(1)
            .GetCreditLimit(Arg.Is(user.Firstname), Arg.Is(user.Surname), Arg.Is(user.DateOfBirth));

        result.Should().Be(expectedResult);
    }

    // Credit Check should return TRUE doing credit check and credit limit is 500 when client name is different from VeryImportantClient and ImportantClient
    // Credit Check should return FALSE doing credit check and credit limit is 499 when client name is different from VeryImportantClient and ImportantClient
    [Xunit.Theory]
    [InlineData(500, true)]
    [InlineData(499, false)]
    public void CreditCheck_ShouldDoCreditCheckOnly_WhenClientNameIsNotVeryImportantClientOrImportantClient(int creditLimit, bool expectedResult)
    {
        // Arrange
        var client = new Client
        {
            Id = 1324,
            Name = "NormalClient",
            ClientStatusEnum = ClientStatusEnum.Gold
        };

        var user = new User()
        {
            Client = client,
            DateOfBirth = DateTime.Parse("1992-01-01"),
            EmailAddress = "lucaspaiva@email.com",
            Firstname = "Lucas",
            Surname = "Paiva"
        };

        _userCreditService.GetCreditLimit(Arg.Is(user.Firstname), Arg.Is(user.Surname), Arg.Is(user.DateOfBirth))
            .Returns(creditLimit);

        // Act
        var result = _sut.CreditCheck(user);

        // Assert
        _userCreditService.Received(1)
            .GetCreditLimit(Arg.Is(user.Firstname), Arg.Is(user.Surname), Arg.Is(user.DateOfBirth));

        result.Should().Be(expectedResult);
    }
}