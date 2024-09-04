using FlowersWhisperingAPI.Plants.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.Plants.Mappers
{
    public class PlantFindMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public int GetPlantId(string name)
        {
            // SQL 查询字符串
            string sql = "SELECT PLANT_ID FROM plant WHERE COMMON_NAME = :PlantName OR SCIENTIFIC_NAME = :PlantName";
            
            try
            {
                // 创建和打开数据库连接
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                
                // 创建 SQL 命令
                using var command = new OracleCommand(sql, connection);
                // 添加参数
                command.Parameters.Add(":PlantName", OracleDbType.Varchar2).Value = name;
                
                // 执行命令并读取结果
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // 获取 PLANT_ID 列的值
                    return reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                }
            }
            catch (Exception ex)
            {
                // 捕获并处理异常
                Console.WriteLine($"Error finding plant: {ex.Message}");
            }
            
            // 如果没有找到或发生错误，返回 -1
            return -1;
        }
        
    }

}