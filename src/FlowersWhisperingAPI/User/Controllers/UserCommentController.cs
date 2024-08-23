using FlowersWhisperingAPI.User.Services.Interface;
using FlowersWhisperingAPI.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCommentController : ControllerBase
    {
        private readonly IUserCommentService _userCommentService;

        public UserCommentController(IUserCommentService userCommentService)
        {
            _userCommentService = userCommentService;
        }

        //用户评论
        [HttpPost("comment")]
        public IActionResult CommentContent(CommentDTO commentDTO)
        {
            bool result = _userCommentService.CommentContent(commentDTO);
            if (result)
                return Ok("评论成功");
            else
                return BadRequest("评论失败");
        }

        //全部评论
        [HttpGet("all/comments")]
        public IActionResult GetAllComments()
        {
            List<Comment> comments = _userCommentService.GetAllComments();
            if(!comments.Any())
                return NoContent();
            return Ok(comments);
        }
    }   
}