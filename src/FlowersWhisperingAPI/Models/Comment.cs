/*评论*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("comment_id")]
        public int CommentId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int? UserId { get; set; } //匿名评论时为null
        public required User User { get; set; }

        [Required]
        [ForeignKey("Plant")]
        [Column("plant_id")]
        public int PlantId { get; set; }
        public required Plant Plant { get; set; }

        [Required]
        [Column("comment_content",TypeName = "CLOB")]
        public required string CommentContent { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}