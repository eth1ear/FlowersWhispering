namespace FlowersWhisperingAPI.User.Services.Interface
{
    public interface IUserAccountService
    {
        bool Login(string username, string password);
    }

}