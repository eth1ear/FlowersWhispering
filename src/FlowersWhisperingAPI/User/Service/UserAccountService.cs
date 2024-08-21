using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Services.Interface;

namespace FlowersWhisperingAPI.src.User.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UserAccountMapper _accountMapper;

        public UserAccountService(UserAccountMapper accountMapper)
        {
            _accountMapper = accountMapper;
        }

        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            try
            {
                bool isValid = _accountMapper.Login(username, password);
                return isValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return false;
            }
        }
    }
}