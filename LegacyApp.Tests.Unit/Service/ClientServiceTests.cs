using FluentAssertions;
using LegacyApp.Model;
using LegacyApp.Repository;
using LegacyApp.Service;
using NSubstitute;
using Xunit;

namespace LegacyApp.Tests.Unit.Service;

public class ClientServiceTests
{
    private readonly IClientService _sut;
    private readonly IClientRepository _clientRepository = Substitute.For<IClientRepository>();

    public ClientServiceTests()
    {
        _sut = new ClientService(_clientRepository);
    }

    [Fact]
    public void GetClientById_ShouldReturnClient_WhenMethodIsCalled()
    {
        // Arrange
        var client = new Client
        {
            Id = 1234,
            Name = "Lucas Paiva",
            ClientStatusEnum = ClientStatusEnum.Gold
        };

        _clientRepository.GetById(Arg.Is(client.Id)).Returns(client);
            
        // Act
        var response = _sut.GetClientById(client.Id);

        // Assert
        _clientRepository.Received(1).GetById(Arg.Is(client.Id));
        response.Should().NotBeNull();
    }
}