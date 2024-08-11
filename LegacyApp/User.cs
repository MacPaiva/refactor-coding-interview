using System;
using LegacyApp.Model;

namespace LegacyApp
{
    // I didn't move this class to Domain folder because it would change the UserDataAccess class.
    public class User
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Client Client { get; set; }
    }
}
