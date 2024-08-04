/*审核*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("review_id")]
        public int ReviewId { get; set; }

        [Required]
        [Column("modified_content",TypeName = "CLOB")]
        public required string ModifiedContent { get; set; }

        [Required]
        [Column("submitted_date")]
        public DateTime SubmittedDate { get; set; }

        [Required]
        [ForeignKey("Submitter")]
        [Column("submitter_id")]
        public int SubmitterId { get; set; }
        public required User Submitter { get; set; }

        [Required]
        [ForeignKey("Plant")]
        [Column("plant_id")]
        public int PlantId { get; set; }
        public required Plant Plant { get; set; }

        [Required]
        [StringLength(20)]
        [Column("review_status")]
        public required string ReviewStatus { get; set; }

        [Column("review_date")]
        public DateTime? ReviewDate { get; set; } //鑻ュ鏍哥姸鎬佷负鈥滄湭瀹℃牳鈥濓紝鍒欎负绌�
    }
}
