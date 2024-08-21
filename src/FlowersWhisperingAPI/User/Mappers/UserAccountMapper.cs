using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.User.Mappers
{
    public class UserAccountMapper
    {
        private readonly string _connectionString;

        public UserAccountMapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Login(string username, string password)
    {
        string sql = "SELECT password FROM account WHERE username = :Username";
        try
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand(sql, connection))
                {
                    command.Parameters.Add(":Username", OracleDbType.Varchar2).Value = username;
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value && result is string stringResult)
                    {
                        string hashedPassword = stringResult;
                            // 可以加密一下
                        return password == hashedPassword;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return false;
    }
    }
}