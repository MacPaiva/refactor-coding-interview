namespace LegacyApp.Service;

public class UserCreditCheckService : IUserCreditCheckService
{
    private readonly IUserCreditService _userCreditService;

    public UserCreditCheckService(IUserCreditService userCreditService)
    {
        _userCreditService = userCreditService;
    }

    public UserCreditCheckService()
    {
        _userCreditService = new UserCreditServiceClient();
    }

    public bool CreditCheck(User user)
    {
        if (user.Client.Name == "VeryImportantClient")
        {
            // Skip credit check
            user.HasCreditLimit = false;
        }
        else
        {
            // Do credit check
            user.HasCreditLimit = true;
            var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
            
            // Double credit limit if Client Name is "ImportantClient"
            user.CreditLimit = (user.Client.Name == "ImportantClient") ? creditLimit * 2 : creditLimit;
        }

        return !user.HasCreditLimit || user.CreditLimit >= 500;
    }
}