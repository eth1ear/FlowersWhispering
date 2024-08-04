/*用户活动日志*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("UserActivityLogs")]
    public class UserActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("log_id")]
        public int LogId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public required User User { get; set; }

        [Required]
        [StringLength(100)]
        public required string ActivityType { get; set; }

        [Required]
        [Column("activity_description",TypeName = "CLOB")]
        public required string ActivityDescription { get; set; }

        [Column("activity_date")]
        public DateTime ActivityDate { get; set; }
    }
}
