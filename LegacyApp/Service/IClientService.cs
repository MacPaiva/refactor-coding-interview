using LegacyApp.Model;

namespace LegacyApp.Service;

public interface IClientService
{
    public Client GetClientById(int clientId);
}