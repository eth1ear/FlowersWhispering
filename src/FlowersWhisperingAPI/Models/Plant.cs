/*植物*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Plants")]
    public class Plant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("plant_id")]
        public int PlantId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("common_name")]
        public required string CommonName { get; set; }

        [Required]
        [StringLength(100)]
        [Column("scientific_name")]
        public required string ScientificName { get; set; }

        [Required]
        [StringLength(100)]
        [Column("category")]
        public required string Category { get; set; }

        [Required]
        [Column("portrayal",TypeName = "CLOB")]
        public required string Portrayal { get; set; }

        [Required]
        [StringLength(255)]
        [Column("growth_environment")]
        public required string GrowthEnvironment { get; set; }

        [Required]
        [Column("care_conditions",TypeName = "CLOB")]
        public required string CareConditions { get; set; }
    }
}
