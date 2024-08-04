/*养护日志*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("CareLogs")]
    public class CareLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("log_id")]
        public int LogId { get; set; }

        [Required]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public required User User { get; set; }

        [Required]
        [ForeignKey("Plant")]
        [Column("plant_id")]
        public int PlantId { get; set; }
        public required Plant Plant { get; set; }

        [Required]
        [StringLength(255)]
        [Column("care_activity")]
        public required string CareActivity { get; set; }

        [Column("log_date")]
        public DateTime LogDate { get; set; }
    }
}
