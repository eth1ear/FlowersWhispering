using FlowersWhisperingAPI.Plants.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.Plants.Mappers
{
    public class PlantContMapper(string connectionString)
    {
        public List<Plant> GetContPlants(int userId)
        {
            List<Plant> plants = new List<Plant>();
            try
            {
                string sql = @"
                            select PLANTS.PLANT_ID, COMMON_NAME, SCIENTIFIC_NAME, CATEGORY, 
                                PORTRAYAL, GROWTH_ENVIRONMENT, CARE_CONDITIONS, 
                                UPDATETIME, PLANTIMAGES.IMAGE_URL
                            from PLANTS 
                            INNER JOIN PLANTIMAGES ON PLANTS.PLANT_ID = PLANTIMAGES.PLANT_ID
                            where PLANTS.PLANT_ID IN(
                                select PLANT_ID
                                from REVIEWS
                                where SUBMITTER_ID = :userId AND REVIEW_STATUS='审核通过')
                            ";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(new OracleParameter("userId", userId));
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int plantId = reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                                string commonName = reader.GetString(reader.GetOrdinal("COMMON_NAME"));
                                string scientificName = reader.GetString(reader.GetOrdinal("SCIENTIFIC_NAME"));
                                string CATEGORY = reader.GetString(reader.GetOrdinal("CATEGORY"));
                                string PORTRAYAL = reader.GetString(reader.GetOrdinal("PORTRAYAL"));
                                string GROWTH_EN = reader.GetString(reader.GetOrdinal("GROWTH_ENVIRONMENT"));
                                string care_con = reader.GetString(reader.GetOrdinal("CARE_CONDITIONS"));
                                DateTime time = reader.GetDateTime(reader.GetOrdinal("UPDATETIME"));
                                string ImageUrl = reader.GetString(reader.GetOrdinal("IMAGE_URL")); 
                                var plant = new Plant(plantId, commonName, scientificName, CATEGORY, PORTRAYAL, GROWTH_EN, care_con, time);
                                plant.ImageUrl = ImageUrl;
                                plants.Add(plant);
                            }
                        }
                    }
                    // 如果成功，返回 plants
                    return plants;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Contribute plants: {ex.Message}");
            }
            // 如果发生错误，返回 null
            return null!;
        }


        public bool AddContPlant(int userId, string plantName, string reviewContent)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // 查询 plantId
                    string querySql = @"
                        SELECT PLANT_ID 
                        FROM PLANTS 
                        WHERE COMMON_NAME = :plantName OR SCIENTIFIC_NAME = :plantName";
                    
                    int plantId;
                    using (OracleCommand queryCommand = new OracleCommand(querySql, connection))
                    {
                        queryCommand.Parameters.Add(new OracleParameter("plantName", plantName));
                        using (OracleDataReader reader = queryCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                plantId = reader.GetInt32(reader.GetOrdinal("PLANT_ID"));
                            }
                            else
                            {
                                Console.WriteLine("Plant not found.");
                                return false;
                            }
                        }
                    }

                    // 插入评论记录
                    string insertSql = @"
                        INSERT INTO REVIEWS (PLANT_ID, SUBMITTER_ID, MODIFIED_CONTENT)
                        VALUES (:plantId, :userId, :reviewContent)";
                    
                    using (OracleCommand insertCommand = new OracleCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.Add(new OracleParameter("plantId", plantId));
                        insertCommand.Parameters.Add(new OracleParameter("userId", userId));
                        insertCommand.Parameters.Add(new OracleParameter("reviewContent", reviewContent));
                        insertCommand.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding contribute plant: {ex.Message}");
            }
            return false;
        }
    }

}