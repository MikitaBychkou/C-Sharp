using System;

namespace LegacyApp
{
    public interface ICreditLimitService
    {
        int GetCreditLimit(string lastName, DateTime birthday);
    }

    public interface IClientRepository
    {
        Client GetById(int IdClient);
    }
    public class UserService
    {

        private IClientRepository _clientRepository;
        private ICreditLimitService _creditLimitService;
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditLimitService = new UserCreditService();
        }

        public UserService(IClientRepository clientRepository,ICreditLimitService limitService)
        {
            _clientRepository = clientRepository;
            _creditLimitService = limitService;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameInCorrect(firstName) || IsLastNameInCorrect(lastName))
            {
                return false;
            }

            if (IsEmailCorrect(email))
            {
                return false;
            }

            var age = CalculateAgeUsingBirthdate(dateOfBirth);

            if (AgeIsLessThan21(age))
            {
                return false;
            }
            
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (IsVeryImportantClient(client))
            {
                user.HasCreditLimit = false;
            }
            else if (IsImportantClient(client))
            {
                using (var userCreditService = new UserCreditService())
                {
                    CalculateCreditLimitForImportantClient(userCreditService, user);
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    CalculateCreditLimitForNormalClient(userCreditService, user);
                }
            }

            if (IsHasCreditLimitAndCreditLimit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static void CalculateCreditLimitForNormalClient(UserCreditService userCreditService, User user)
        {
            int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            user.CreditLimit = creditLimit;
        }

        private static void CalculateCreditLimitForImportantClient(UserCreditService userCreditService, User user)
        {
            int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            creditLimit *= 2;
            user.CreditLimit = creditLimit;
        }

        private static bool IsHasCreditLimitAndCreditLimit(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private static bool IsImportantClient(Client client)
        {
            return client.Type == "ImportantClient";
        }

        private static bool IsVeryImportantClient(Client client)
        {
            return client.Type == "VeryImportantClient";
        }

        private static bool AgeIsLessThan21(int age)
        {
            return age < 21;
        }

        private static int CalculateAgeUsingBirthdate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        private static bool IsEmailCorrect(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        public static bool IsLastNameInCorrect(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        public static bool IsFirstNameInCorrect(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}
