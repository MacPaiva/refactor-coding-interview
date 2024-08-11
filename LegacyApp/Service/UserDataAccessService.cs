namespace LegacyApp.Service;

public class UserDataAccessService : IUserDataAccessService
{
    public void AddUser(User user)
    {
        UserDataAccess.AddUser(user);
    }
}