using System;

namespace FlowersWhisperingAPI.User.Models
{
    public class UserInfo(string username, string password, string email, string languagePreference, string userStatus = "active", string userRole = "user", int userId = 0)
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
        public string Email { get; set; } = email;
        public string UserStatus { get; set; } = userStatus;
        public string UserRole { get; set; } = userRole;
        public string LanguagePreference { get; set; } = languagePreference;

        public int UserId { get; set; } = userId;
    }
}