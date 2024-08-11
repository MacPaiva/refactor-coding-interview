using LegacyApp.Model;

namespace LegacyApp.Repository;

public interface IClientRepository
{
    public Client GetById(int id);
}