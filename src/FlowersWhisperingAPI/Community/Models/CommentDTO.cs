namespace FlowersWhisperingAPI.Community.Models{

    public class CommentDTO
    {
        public int UserId { get; set; } = 0;
        public int ArticleId { get; set; } = 0;
        public string? Content { get; set; } = null;

    }

}