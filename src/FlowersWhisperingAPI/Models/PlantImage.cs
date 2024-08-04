/*植物图片*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("PlantImages")]
    public class PlantImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("image_id")]
        public int ImageId { get; set; }

        [Required]
        [ForeignKey("Plant")]
        [Column("plant_id")]
        public int PlantId { get; set; }
        public required Plant Plant { get; set; }

        [Required]
        [ForeignKey("User")]
        [Column("user_id")]
        public int? UserId { get; set; } //鍖垮悕涓婁紶鏃朵负null
        public required User User { get; set; }

        [Required]
        [StringLength(255)]
        [Column("image_url")]
        public required string ImageUrl { get; set; }

        [Column("recognition_result",TypeName = "CLOB")]
        public string? RecognitionResult { get; set; } //鑻ラ渶姹備负璇嗗埆锛屽垯姝ゅ瓧娈典笉涓虹┖

        [Column("diagnosis_result",TypeName = "CLOB")]
        public string? DiagnosisResult { get; set; } //鑻ラ渶姹備负璇婃柇锛屽垯姝ゅ瓧娈典笉涓虹┖

        [Column("upload_date")]
        public DateTime UploadDate { get; set; }
    }
}
