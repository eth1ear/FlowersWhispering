using FlowersWhisperingAPI.Plants.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.Plants.Mappers
{
    public class PlantFavorMapper(string connectionString)
    {
        public List<Plant> GetFavorPlants(int userId)
        {
            List<Plant> plants = new List<Plant>();
            
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Plant WHERE PLANT_ID IN (SELECT PLANT_ID FROM Favorites WHERE USER_ID = :userId)";
                    
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
                                DateTime time = reader.GetDateTime(reader.GetOrdinal("UPDATE_TIME"));
                                
                                var plant = new Plant(plantId, commonName, scientificName, CATEGORY, PORTRAYAL, GROWTH_EN, care_con, time);
                                plants.Add(plant);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving favorite plants: {ex.Message}");
            }
            
            return plants;
        }

    }

}