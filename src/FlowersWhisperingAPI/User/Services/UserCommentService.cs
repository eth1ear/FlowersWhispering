using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Models;
using FlowersWhisperingAPI.User.Services.Interface;

namespace FlowersWhisperingAPI.User.Services
{
    public class UserCommentService(UserCommentMapper commentMapper) : IUserCommentService
    {
        private readonly UserCommentMapper _commentMapper = commentMapper;

        public bool CommentContent(CommentDTO commentDTO)
        {
            return _commentMapper.InsertComment(commentDTO.UserId, commentDTO.PlantId, commentDTO.CommentContent);
        }

        public bool DeleteComment(int commentId)
        {
            return _commentMapper.DeleteComment(commentId);
        }

        public List<Comment> GetAllComments()
        {
            return _commentMapper.GetAllComments();
        }

        public bool UpdateComment(EditCommentDTO editCommentDTO)
        {
            return _commentMapper.UpdateComment(editCommentDTO);
        }
    }

}