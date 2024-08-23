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

        public int UserId { get; set; } = 0;

        public UserInfo(string username, string password, string email, string languagePreference, string userStatus = "active", string userRole = "user", int userId = 0)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            UserStatus = userStatus;
            UserRole = userRole;
            LanguagePreference = languagePreference;
        }
    }
}