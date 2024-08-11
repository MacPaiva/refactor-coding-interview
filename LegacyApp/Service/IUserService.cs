using System;

namespace LegacyApp.Service;

public interface IUserService
{
    public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId);
}