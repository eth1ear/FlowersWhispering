using FlowersWhisperingAPI.User.Models;

namespace FlowersWhisperingAPI.User.Services.Interface
{
    public interface IUserCommentService
    {
        public bool CommentContent(CommentDTO commentDTO);

        public List<Comment> GetAllComments();

        public bool DeleteComment(int commentId);

        public bool UpdateComment(EditCommentDTO editCommentDTO);
    }

}