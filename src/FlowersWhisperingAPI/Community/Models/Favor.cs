namespace FlowersWhisperingAPI.Community.Models
{
    public class Favor(int favorId,int userId, int articleId)
    {
        public int FavorId { get; set; } = favorId;
        public int UserId { get; set; } = userId;
        public int ArticleId { get; set; } = articleId;
     
    }
}
