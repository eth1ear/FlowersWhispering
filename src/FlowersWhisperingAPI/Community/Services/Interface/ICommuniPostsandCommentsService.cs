using FlowersWhisperingAPI.Community.Models;

namespace FlowersWhisperingAPI.Community.Services.Interface;

public interface ICommuniPostsandCommentsService
{
    public List<Article> GetAllPosts();
    public List<Article> GetAllPostsByUserId(int userId);
    public List<Article> GetAllPostsByTitleOrContent(string text);
    public void AddPost(ArticleDTO post);
    public void DeletePost(int postId);
    public void UpdatePost(Article post);
    
    public List<Comment> GetAllComments(int postId);

    public void AddComment(CommentDTO commentdto);

    public void DeleteComment(int commentId);

}

