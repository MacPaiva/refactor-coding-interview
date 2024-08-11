using System;
using LegacyApp.Service;
using LegacyApp.Utils;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IClientService _clientService;
        private readonly IUserCreditCheckService _userCreditCheckService;
        private readonly IUserDataAccessService _userDataAccessService;
        
        // Created this constructor to still be able to call UserService without changing Program.cs class.
        public UserService()
        {
            _clientService = new ClientService();
            _userCreditCheckService = new UserCreditCheckService();
            _userDataAccessService = new UserDataAccessService();
        }

        public UserService(IClientService clientService, IUserCreditCheckService userCreditCheckService,
            IUserDataAccessService userDataAccessService)
        {
            _clientService = clientService;
            _userCreditCheckService = userCreditCheckService;
            _userDataAccessService = userDataAccessService;
        }
        
        // I didn't move this class to Service folder because it would change the Program.cs class.
        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (!ValidateUserInformation(firname, surname, email, dateOfBirth)) return false;
            
            var client = _clientService.GetClientById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };
            
            if (!_userCreditCheckService.CreditCheck(user)) return false;
            
            _userDataAccessService.AddUser(user);

            return true;
        }

        private bool ValidateUserInformation(string firname, string surname, string email, DateTime dateOfBirth)
        {
            return ValidateUserName(firname, surname) && ValidateUserEmail(email) && ValidateUserAge(dateOfBirth);
        }

        private bool ValidateUserAge(DateTime dateOfBirth)
        {
            return DateUtils.CalculateAge(dateOfBirth) >= 21;
        }

        private bool ValidateUserEmail(string email)
        {
            return !(!email.Contains("@") && !email.Contains("."));
        }

        private bool ValidateUserName(string firname, string surname)
        {
            return !(string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname));
        }
    }
}
