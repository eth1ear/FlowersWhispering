/*用户积分*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("UserPoints")]
    public class UserPoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("points_id")]
        public int PointsId { get; set; }

        [Required]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public required User User { get; set; }

        [Column("points")]
        public int Points { get; set; } = 0; //绉垎榛樿涓洪浂

        [Column("last_updated")]
        public DateTime? LastUpdated { get; set; } //鑻ヤ粠鏈幏寰楃Н鍒嗭紝鍒欐渶鍚庢洿鏂版椂闂村彲涓簄ull
    }
}
