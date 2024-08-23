namespace FlowersWhisperingAPI.User.Models
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LanguagePreference { get; set; } = null!;
        public int UserId { get; set; } = 0;
    }
}