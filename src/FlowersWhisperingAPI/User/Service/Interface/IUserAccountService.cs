using FlowersWhisperingAPI.User.Models;

namespace FlowersWhisperingAPI.User.Services.Interface
{
    public interface IUserAccountService
    {
        string GetPasswordByUsername(string username);
        string GetPasswordByEmail(string email);
        bool Register(UserInfo userRegister);
        bool UserUpdate(UserDTO registerDTO);
        bool IsHaveUsername(string username);
        bool IsHaveEmail(string email);

        string GetUsernameByEmail(string email);
    }

}