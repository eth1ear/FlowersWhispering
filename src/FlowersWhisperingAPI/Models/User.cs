/*用户*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("user_name")]
        public required string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("password")]
        public required string Password { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email")]
        public required string Email { get; set; }

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }= DateTime.UtcNow.AddHours(8);

        [Required]
        [StringLength(20)]
        [Column("user_role")]
        public required string UserRole { get; set; } = "User";

        [Required]
        [StringLength(20)]
        [Column("user_status")]
        public required string UserStatus { get; set; } = "Active";

        [StringLength(20)]
        [Column("language_preference")] 
        public required string LanguagePreference { get; set; } = "cn-ZH";
    }
}
