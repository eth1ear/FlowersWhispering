using FlowersWhisperingAPI.User.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.User.Mappers
{
    public class UserAccountMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public UserInfo? GetUserByUsername(string username)
        {
            string sql = "SELECT * FROM users WHERE username = :Username";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = username;
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // 从数据读取器中提取用户信息
                int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                string usernameResult = reader.GetString(reader.GetOrdinal("username"));
                string passwordResult = reader.GetString(reader.GetOrdinal("password"));
                string emailResult = reader.GetString(reader.GetOrdinal("email"));
                string languagePreferenceResult = reader.GetString(reader.GetOrdinal("language_preference"));
                string userStatusResult = reader.GetString(reader.GetOrdinal("user_status"));
                string userRoleResult = reader.GetString(reader.GetOrdinal("user_role"));
                UserInfo userInfo = new UserInfo(usernameResult, passwordResult, emailResult, languagePreferenceResult, userStatusResult, userRoleResult, userId);
                return userInfo;  // 返回 UserInfo 对象
            }
            return null;  // 如果查询结果为空或者出现异常，则返回 null
        }

        public UserInfo? GetUserById(int id)
        {
            string sql = "SELECT * FROM users WHERE user_id = :Id";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Id", OracleDbType.Int32).Value = id;
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // 从数据读取器中提取用户信息
                int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                string usernameResult = reader.GetString(reader.GetOrdinal("username"));
                string passwordResult = reader.GetString(reader.GetOrdinal("password"));
                string emailResult = reader.GetString(reader.GetOrdinal("email"));
                string languagePreferenceResult = reader.GetString(reader.GetOrdinal("language_preference"));
                string userStatusResult = reader.GetString(reader.GetOrdinal("user_status"));
                string userRoleResult = reader.GetString(reader.GetOrdinal("user_role"));
                UserInfo userInfo = new(usernameResult, passwordResult, emailResult, languagePreferenceResult, userStatusResult, userRoleResult, userId);
                return userInfo;  // 返回 UserInfo 对象
            }
            return null;  // 如果查询结果为空或者出现异常，则返回 null
        }

        public UserInfo? GetUserByEmail(string email)
        {
            string sql = "SELECT * FROM USERS WHERE email = :Email";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Email", OracleDbType.Varchar2).Value = email;
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // 从数据读取器中提取用户信息
                int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                string usernameResult = reader.GetString(reader.GetOrdinal("username"));
                string passwordResult = reader.GetString(reader.GetOrdinal("password"));
                string emailResult = reader.GetString(reader.GetOrdinal("email"));
                string languagePreferenceResult = reader.GetString(reader.GetOrdinal("language_preference"));
                string userStatusResult = reader.GetString(reader.GetOrdinal("user_status"));
                string userRoleResult = reader.GetString(reader.GetOrdinal("user_role"));
                UserInfo userInfo = new UserInfo(usernameResult, passwordResult, emailResult, languagePreferenceResult, userStatusResult, userRoleResult, userId);
                return userInfo;  // 返回 UserInfo 对象
            }
            return null;  // 如果查询结果为空或者出现异常，则返回 null
        }

        public bool InsertUser(UserInfo userInfo)
        {
            string sql = @"
                INSERT INTO users (username, password, email, user_status, user_role, language_preference)
                VALUES (:Username, :Password, :Email, :UserStatus, :UserRole, :LanguagePreference)";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = userInfo.Username;
                command.Parameters.Add(":Password", OracleDbType.Varchar2).Value = userInfo.Password;
                command.Parameters.Add(":Email", OracleDbType.Varchar2).Value = userInfo.Email;
                command.Parameters.Add(":UserStatus", OracleDbType.Varchar2).Value = userInfo.UserStatus;
                command.Parameters.Add(":UserRole", OracleDbType.Varchar2).Value = userInfo.UserRole;
                command.Parameters.Add(":LanguagePreference", OracleDbType.Varchar2).Value = userInfo.LanguagePreference;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error inserting user: {ex.Message}");
               return false; // 插入失败
            }           
        } 

        public bool UserUpdate(UserDTO userDTO)
        {
            string sql = @"UPDATE users SET username = :Username, password = :Password, email = :Email, 
                language_preference = :LanguagePreference WHERE user_id = :UserId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = userDTO.Username;
                command.Parameters.Add(":Password", OracleDbType.Varchar2).Value = userDTO.Password;
                command.Parameters.Add(":Email", OracleDbType.Varchar2).Value = userDTO.Email;
                command.Parameters.Add(":LanguagePreference", OracleDbType.Varchar2).Value = userDTO.LanguagePreference;
                command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userDTO.UserId;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false; // 更新失败
            }
        }

        public bool IsHaveEmail(string email)
        {
            string sql = "SELECT COUNT(*) FROM users WHERE email = :Email";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Email", OracleDbType.Varchar2).Value = email;
            var result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value && Convert.ToInt32(result) > 0)
                return true;
            return false; 
        }

        public bool IsHaveUsername(string username)
        {
            string sql = "SELECT COUNT(*) FROM users WHERE username = :Username";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = username;
            var result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value && Convert.ToInt32(result) > 0)
                return true;
            return false; 
        }

        public string? GetUsernameByUserId(int userId)
        {
            string sql = "SELECT username FROM users WHERE user_id = :UserId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":UserId", OracleDbType.Int32).Value = userId;
            var result = command.ExecuteScalar();
            return result != null && result != DBNull.Value ? result.ToString(): null;                       
        }    
    }
}