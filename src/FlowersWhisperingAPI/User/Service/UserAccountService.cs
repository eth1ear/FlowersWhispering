using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Models;
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

        public string GetPasswordByUsername(string username)
        {
            try
            {
                var user = _accountMapper.GetUserByUsername(username);
                if(user == null)
                    return "";
                //可以加密下
                return user.Password;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return "";
            }
        }

        public string GetPasswordByEmail(string email)
        {
            try
            {
                var user = _accountMapper.GetUserByEmail(email);
                if(user == null)
                    return "";
                //可以加密下
                return user.Password;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                return "";
            }
        }

       public bool IsHaveEmail(string email)
        {
            bool result = _accountMapper.IsHaveEmail(email);
            return result;
        }

        public bool IsHaveUsername(string username)
        {
            bool result = _accountMapper.IsHaveUsername(username);
            return result;
        }

        public bool Register(UserInfo userInfo)
        {
            try
            {
                bool result = _accountMapper.InsertUser(userInfo);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                return false;
            }
        }

        public bool UserUpdate(UserDTO registerDTO)
        {
            try
            {
                bool result = _accountMapper.UserUpdate(registerDTO);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                return false;
            }
        }

        public string GetUsernameByEmail(string email)
        {
            var result = _accountMapper.GetUserByEmail(email);
            if(result == null)
                return "";
            string username = result.Username;
            return username;
        }
    }
}