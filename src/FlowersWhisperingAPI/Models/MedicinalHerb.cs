/*中草药*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("MedicinalHerbs")]
    public class MedicinalHerb
    {
        [Key]
        [Column("herb_id")]
        public int HerbId { get; set; }

        [ForeignKey("Plant")]
        [Column("plant_id")]
        public int PlantId { get; set; }
        public required Plant Plant { get; set; }

        [Required]
        [Column("medicinal_properties",TypeName = "CLOB")]
        public required string MedicinalProperties { get; set; }

        [Required]
        [Column("usage_instructions",TypeName = "CLOB")]
        public required string UsageInstructions { get; set; }
    }
}
