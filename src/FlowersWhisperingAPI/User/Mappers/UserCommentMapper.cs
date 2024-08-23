using FlowersWhisperingAPI.User.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace FlowersWhisperingAPI.User.Mappers
{
    public class UserCommentMapper
    {
        private readonly string _connectionString;

        public UserCommentMapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool InsertComment(CommentDTO commentDTO)
        {
            string sql = "INSERT INTO comments (user_id, plant_id, comment_content) VALUES (:UserId, :PlantId, :CommentContent)";
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":UserId", OracleDbType.Int32).Value = commentDTO.UserId;
                        command.Parameters.Add(":PlantId", OracleDbType.Int32).Value = commentDTO.PlantId;
                        command.Parameters.Add(":CommentContent", OracleDbType.Varchar2).Value = commentDTO.CommentContent;
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Error inserting user: {ex.Message}");
               return false; // 插入失败
            }           
        } 

        public List<Comment> GetAllComments()
        {
            List<Comment> comments = [];
            string sql = "SELECT * FROM comments";
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand(sql, connection))
                    {                       
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int CommentId = reader.GetInt32(reader.GetOrdinal("comment_id"));                                
                                int UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                                int PlantId = reader.GetInt32(reader.GetOrdinal("plant_id"));
                                string CommentContent = reader.GetString(reader.GetOrdinal("comment_content"));
                                DateTime CommentTime = reader.GetDateTime(reader.GetOrdinal("created_date"));
                                Comment comment = new Comment(CommentId,UserId,PlantId,CommentContent,CommentTime);
                                //Console.WriteLine($"CommentId: {CommentId}, UserId: {UserId}, PlantId: {PlantId}, CommentContent: {CommentContent}, CommentTime: {CommentTime}"); 
                                comments.Add(comment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving comments: {ex.Message}");
            }
            return comments;
        }
    }
}