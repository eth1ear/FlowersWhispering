namespace FlowersWhisperingAPI.User.Models
{
    public class Comment
    {
        public int CommentId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int PlantId { get; set; } = 0;
        public string CommentContent { get; set; } = null!;  
        public DateTime CommentTime { get; set; } = DateTime.UtcNow.AddHours(8);

        public Comment(int commentId, int userId, int plantId, string commentContent, DateTime commentTime)
        {
            CommentId = commentId;
            UserId = userId;
            PlantId = plantId;
            CommentContent = commentContent;
            CommentTime = commentTime;
        } 
    }   
}