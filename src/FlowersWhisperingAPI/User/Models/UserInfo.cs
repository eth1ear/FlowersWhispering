using System;

namespace FlowersWhisperingAPI.User.Models
{
    public class UserInfo
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserStatus { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public string LanguagePreference { get; set; } = null!;

        public UserInfo(string username, string password, string email, string languagePreference, string userStatus = "active", string userRole = "user")
        {
            Username = username;
            Password = password;
            Email = email;
            UserStatus = userStatus;
            UserRole = userRole;
            LanguagePreference = languagePreference;
        }
    }
}