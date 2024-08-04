/*地区适宜性*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("PlantRegionSuitability")]
    public class PlantRegionSuitability
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("suitability_id")]
        public int SuitabilityId { get; set; }

        [ForeignKey("Plant")]
        [Column("plant_id")]
        public int PlantId { get; set; }
        public required Plant Plant { get; set; }

        [Required]
        [StringLength(100)]
        [Column("region_name")]
        public required string RegionName { get; set; }

        [Required]
        [StringLength(255)]
        [Column("suitability")]
        public required string Suitability { get; set; }
    }
}
