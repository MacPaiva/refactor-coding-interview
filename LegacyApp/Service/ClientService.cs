using LegacyApp.Model;
using LegacyApp.Repository;

namespace LegacyApp.Service;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public ClientService()
    {
        _clientRepository = new ClientRepository();
    }

    public Client GetClientById(int clientId)
    {
        return _clientRepository.GetById(clientId);
    }
}