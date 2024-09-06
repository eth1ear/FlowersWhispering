using FlowersWhisperingAPI.Plants.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.Plants.Mappers
{
    public class PlantLatestMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public List<Plant> GetLatestPlants()
        {
            var plants = new List<Plant>();
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM ( SELECT * FROM PLANT ORDER BY PLANT_ID DESC ) WHERE ROWNUM <= 8";

                    using (var command = new OracleCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 从数据库中读取数据
                            int plantId = reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                            string commonName = reader.GetString(reader.GetOrdinal("COMMON_NAME"));
                            string scientificName = reader.GetString(reader.GetOrdinal("SCIENTIFIC_NAME"));
                            string category = reader.GetString(reader.GetOrdinal("CATEGORY"));
                            string portrayal = reader.GetString(reader.GetOrdinal("PORTRAYAL"));
                            string growthEnvironment = reader.GetString(reader.GetOrdinal("GROWTH_ENVIRONMENT"));
                            string careConditions = reader.GetString(reader.GetOrdinal("CARE_CONDITIONS"));
                            DateTime updateTime = reader.GetDateTime(reader.GetOrdinal("UPDATETIME"));

                            // 创建 Plant 对象并添加到列表
                            var plant = new Plant(plantId, commonName, scientificName, category, portrayal, growthEnvironment, careConditions, updateTime);
                            plants.Add(plant);
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                // 处理 Oracle 数据库异常
                Console.WriteLine($"OracleException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return plants;
        }
    }

}