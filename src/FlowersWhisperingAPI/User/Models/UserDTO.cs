namespace FlowersWhisperingAPI.User.Models
{
    public class UserDTO
    {
        public UserDTO(string Username, string Password, string Email, string LanguagePreference)
        {
            this.Username = Username;
            this.Password = Password;
            this.Email = Email;
            this.LanguagePreference = LanguagePreference;
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LanguagePreference { get; set; } = null!;
    }
}