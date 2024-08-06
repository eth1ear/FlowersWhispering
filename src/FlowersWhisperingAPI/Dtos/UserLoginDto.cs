using System.ComponentModel.DataAnnotations;

public class UserLoginDto
{
    [Required]
    public required string Login { get; set; } // 用户名或邮箱

    [Required]
    public required string Password { get; set; }
}