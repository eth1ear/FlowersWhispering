using System.Data;
using FlowersWhisperingAPI.Community.Models;
using Oracle.ManagedDataAccess.Client;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FlowersWhisperingAPI.Community.Mappers
{
    public class CommuniPostsandCommentsMapper(string connectionString)
    {
        private readonly string _connectionString = connectionString;

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
        public void AddComment(CommentDTO commentdto)
        {
            string sql = @"INSERT INTO Comments (user_id, article_id, comment_content, 
            ) VALUES (:UserId, :ArticleId, :Contentd)";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":UserId", OracleDbType.Int32).Value = commentdto.UserId;
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = commentdto.ArticleId;
            command.Parameters.Add(":Contentd", OracleDbType.Clob).Value = commentdto.Content;
            command.ExecuteNonQuery();

        }
        public void AddPost(ArticleDTO post)
        {
            string sql = @"INSERT INTO Articles (article_title, article_content, publisher_id, 
                article_type) VALUES (:Title, :Content, :PublisherId, 'article')";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Title", OracleDbType.Varchar2).Value = post.Title;
            command.Parameters.Add(":Content", OracleDbType.Clob).Value = post.Content;
            command.Parameters.Add(":PublisherId", OracleDbType.Int32).Value = post.PublisherId;
            command.ExecuteNonQuery(); 
        }
        public void DeleteComment(int commentId)
        {
            string sql = "DELETE FROM Comments WHERE comment_id = :commentId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":commentId", OracleDbType.Int32).Value = commentId;
            command.ExecuteNonQuery();  

        }
        public void DeletePost(int postId)
        {
            string sql = "DELETE FROM Articles WHERE article_id = :ArticleId AND article_type = 'article'";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = postId;
            command.ExecuteNonQuery();  

        }
        public List<Comment> GetAllComments(int postId)
        {
            List<Comment> comments = [];
            string sql = "SELECT * FROM Comments WHERE article_id = :postId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":postId", OracleDbType.Int32).Value = postId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {                  
                    int cid = reader.GetInt32(0);
                    int uid = reader.GetInt32(1);
                    string content = reader.GetString(3);
                    DateTime date = reader.GetDateTime(4);
                    string? username = GetUsernameByUserId(uid);
                    if (username == null)
                    {
                        throw new Exception($"Username not found for userId: {uid}");
                    }
                    comments.Add(new Comment(cid, uid, postId, content, date){ Username = username });
                }
                return comments;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }
          
        }
        
        public List<Article> GetAllPosts()
        {
            List<Article> posts = [];
            string sql = "SELECT * FROM Articles WHERE article_type = 'article'";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string content = reader.GetString(2);
                    int pid = reader.GetInt32(3);
                    DateTime date = reader.GetDateTime(4);
                    string? username = GetUsernameByUserId(pid);
                    if (username == null)
                    {
                        throw new Exception($"Username not found for userId: {pid}");
                    }
                    posts.Add(new Article(id, title, content, pid, date){ Username = username });
                }
                return posts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }
        }
        public List<Article> GetAllPostsByUserId(int userId)
        {
            List<Article> posts = [];
            string sql = "SELECT * FROM Articles WHERE publisher_id = :userId";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {                  
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string content = reader.GetString(2);
                    int pid = reader.GetInt32(3);
                    DateTime date = reader.GetDateTime(4);
                    posts.Add(new Article(id, title, content, pid, date));
                }
                return posts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }          

        }
        public List<Article> GetAllPostsByTitleOrContent(string text)
        {
            List<Article> posts = [];
            string sql = "SELECT * FROM Articles WHERE article_title like '%:text%' OR article_content like '%:text%'";
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using var command = new OracleCommand(sql, connection);
                command.Parameters.Add(":text", OracleDbType.Int32).Value = text;
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {                  
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string content = reader.GetString(2);
                    int pid = reader.GetInt32(3);
                    DateTime date = reader.GetDateTime(4);
                    string? username = GetUsernameByUserId(pid);
                    if (username == null)
                    {
                        throw new Exception($"Username not found for userId: {pid}");
                    }
                    posts.Add(new Article(id, title, content, pid, date){ Username = username });
                }
                return posts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return [];
            }      

        }
        public void UpdatePost(Article post)
        {
            string sql = "UPDATE article SET article_title = :Title, article_content = :Content , published_date = SYSDATE WHERE article_id = :ArticleId";
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":Title", OracleDbType.Varchar2).Value = post.Title;
            command.Parameters.Add(":Content", OracleDbType.Clob).Value = post.Content;
            command.Parameters.Add(":ArticleId", OracleDbType.Int32).Value = post.ArticleId;
            command.ExecuteNonQuery();

        }
    }
}

