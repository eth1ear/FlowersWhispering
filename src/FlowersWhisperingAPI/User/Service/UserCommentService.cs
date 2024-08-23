using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Models;
using FlowersWhisperingAPI.User.Services.Interface;

namespace FlowersWhisperingAPI.User.Services
{
    public class UserCommentService : IUserCommentService
    {
        private readonly UserCommentMapper _commentMapper;

        public UserCommentService(UserCommentMapper commentMapper)
        {
            _commentMapper = commentMapper;
        }

        public bool CommentContent(CommentDTO commentDTO)
        {
            try
            {
                bool result = _commentMapper.InsertComment(commentDTO);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                return false;
            }
        }

        public List<Comment> GetAllComments()
        {
            return _commentMapper.GetAllComments();
        }
    }

}