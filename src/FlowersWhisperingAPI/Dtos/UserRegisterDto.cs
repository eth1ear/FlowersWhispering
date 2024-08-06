using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [Required]
    [StringLength(50)]
    public required string UserName { get; set; }

    [Required]
    [StringLength(50)]
    public required string Password { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }
}
