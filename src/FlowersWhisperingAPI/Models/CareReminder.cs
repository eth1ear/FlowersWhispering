/*养护提醒服务*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("CareReminders")]
    public class CareReminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("reminder_id")]
        public int ReminderId { get; set; }

        [ForeignKey("Plant")]
        [Column("plant_id")]
        public int PlantId { get; set; }
        public required Plant Plant { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public required User User { get; set; }

        [Required]
        [StringLength(255)]
        public required string ReminderText { get; set; }

        [Column("reminder_date")]
        public DateTime ReminderDate { get; set; }

        [Required]
        public char IsActive { get; set; }
    }
}
