/*收藏*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Favorites")]
    public class Favorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("favorite_id")]
        public int FavoriteId { get; set; }

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
    }
}
