using FlowersWhisperingAPI.Community.Models;
using FlowersWhisperingAPI.Community.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.Community.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommuniPostsandCommentsController(ICommuniPostsandCommentsService communiPostsandCommentsService) : ControllerBase
{
    [HttpPost("comment/add")]
    public void AddComment([FromBody] CommentDTO commentdto)
    {
        communiPostsandCommentsService.AddComment(commentdto);
    }

    [HttpPost("post/add")]
    public void AddPost([FromBody] ArticleDTO post)
    {
        communiPostsandCommentsService.AddPost(post);
    }

    [HttpDelete("comment/delete")]
    public void DeleteComment(int commentId)
    {
        communiPostsandCommentsService.DeleteComment(commentId);
    }

    [HttpDelete("post/delete")]
    public void DeletePost(int postId)
    {
        communiPostsandCommentsService.DeletePost(postId);
    }
//某个帖子的所有评论
    [HttpGet("comment/all")]
    public IActionResult GetAllComments(int postId)
    {
        return Ok(communiPostsandCommentsService.GetAllComments(postId));
    }

    [HttpGet("post/all")] 
    public IActionResult GetAllPosts()
    {
        return Ok(communiPostsandCommentsService.GetAllPosts());
    }

    [HttpGet("post/user/all")]
    public IActionResult GetAllPostsByUserId(int userId)
    {
        return Ok(communiPostsandCommentsService.GetAllPostsByUserId(userId));
    }

    [HttpGet("post/all/search")]
    public IActionResult GetAllPostsByTitleOrContent(string text)
    {
        return Ok(communiPostsandCommentsService.GetAllPostsByTitleOrContent(text));
    }

    [HttpPut("post/edit")]
    public void UpdatePost([FromBody] Article post)
    {
        communiPostsandCommentsService.UpdatePost(post);
    }

}