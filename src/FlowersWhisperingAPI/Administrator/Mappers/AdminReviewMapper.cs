using FlowersWhisperingAPI.Administrator.Models;
using Oracle.ManagedDataAccess.Client;

namespace FlowersWhisperingAPI.Administrator.Mappers
{
    public class AdminReviewMapper(string connectionString) 
    {
        private readonly string _connectionString = connectionString;

        public List<Review> GetAllReviews()
        {
            List<Review> reviews = [];
            string sql = "SELECT * FROM Reviews";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int reviewId = reader.GetInt32(0);
                    int plantId = reader.GetInt32(1);
                    int submitterId = reader.GetInt32(2);
                    string content = reader.GetString(3);
                    string reviewStatus = reader.GetString(4);
                    DateTime submittedDate = reader.GetDateTime(5);
                    DateTime? reviewDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6);
                    Review review = new(reviewId, plantId, submitterId, content, reviewStatus, submittedDate, reviewDate);
                    reviews.Add(review);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }
            return reviews;
        }

        public void UpdateReviewStatus(int reviewId, string reviewStatus)
        {
            string sql = "UPDATE Reviews SET review_status = :ReviewStatus, review_date = SYSDATE WHERE review_id = :ReviewId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":ReviewStatus", OracleDbType.Varchar2).Value = reviewStatus;
            command.Parameters.Add(":ReviewId", OracleDbType.Int32).Value = reviewId;
            command.ExecuteNonQuery();
        }

        public void AddPlant(string commonName, string scientificName, string category, string portrayal, string growthEnvironment, string careConditions)
        {
            string sql = "INSERT INTO Plants (common_name, scientific_name, category, portrayal, growth_environment, care_conditions) " +
                    "VALUES (:CommonName, :ScientificName, :Category, :Portrayal, :GrowthEnvironment, :CareConditions)";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":CommonName", OracleDbType.Varchar2).Value = commonName;
            command.Parameters.Add(":ScientificName", OracleDbType.Varchar2).Value = scientificName;
            command.Parameters.Add(":Category", OracleDbType.Varchar2).Value = category;
            command.Parameters.Add(":Portrayal", OracleDbType.Clob).Value = portrayal;
            command.Parameters.Add(":GrowthEnvironment", OracleDbType.Varchar2).Value = growthEnvironment;
            command.Parameters.Add(":CareConditions", OracleDbType.Clob).Value = careConditions;
            command.ExecuteNonQuery();
        }

        public void UpdatePlant(int plantId, string commonName, string scientificName, string category, string portrayal, string growthEnvironment, string careConditions)
        {
            string sql = "UPDATE Plants SET common_name = :CommonName, scientific_name = :ScientificName," +
                        "category = :Category, portrayal = :Portrayal, growth_environment = :GrowthEnvironment, " +
                        "care_conditions = :CareConditions WHERE plant_id = :PlantId";
            using OracleConnection connection = new OracleConnection(_connectionString);
            connection.Open();
            using OracleCommand command = new OracleCommand(sql, connection);              
            command.Parameters.Add(":CommonName", OracleDbType.Varchar2).Value = commonName;
            command.Parameters.Add(":ScientificName", OracleDbType.Varchar2).Value = scientificName;
            command.Parameters.Add(":Category", OracleDbType.Varchar2).Value = category;
            command.Parameters.Add(":Portrayal", OracleDbType.Clob).Value = portrayal;
            command.Parameters.Add(":GrowthEnvironment", OracleDbType.Varchar2).Value = growthEnvironment;
            command.Parameters.Add(":CareConditions", OracleDbType.Clob).Value = careConditions;
            command.Parameters.Add(":PlantId", OracleDbType.Int32).Value = plantId;
            command.ExecuteNonQuery();
        }

        public void DeletePlant(int plantId)
        {
            string sql = "DELETE FROM Plants WHERE plant_id = :PlantId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":PlantId", OracleDbType.Int32).Value = plantId;
            command.ExecuteNonQuery();
        }

        public Plant? SelectPlantById(int plantId)
        {
            string sql = "SELECT * FROM Plants WHERE plant_id = :PlantId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":PlantId", OracleDbType.Int32).Value = plantId;
                using var reader = command.ExecuteReader(); 
                if(reader.Read())
                {
                    string commonName = reader.GetString(1);
                    string scientificName = reader.GetString(2);
                    string category = reader.GetString(3);
                    string portrayal = reader.GetString(4);
                    string growthEnvironment = reader.GetString(5);
                    string careConditions = reader.GetString(6); 
                    return new Plant(plantId, commonName, scientificName, category, portrayal, growthEnvironment, careConditions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error selecting record: " + ex.Message);             
            }
            return null;
        }
    }
}