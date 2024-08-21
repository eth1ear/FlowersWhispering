namespace FlowersWhisperingAPI.User.Models
{
    public class Account
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Account(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}