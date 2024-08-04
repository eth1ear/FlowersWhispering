/*用户反馈*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("feedback_id")]
        public int FeedbackId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int? UserId { get; set; } //匿名提交时为null
        public required User User { get; set; }

        [Required]  
        [Column("feedback_content",TypeName = "CLOB")]
        public required string FeedbackContent { get; set; }

        [Column("submitted_date")]
        public DateTime SubmittedDate { get; set; }
    }
}
