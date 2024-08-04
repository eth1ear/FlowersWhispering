/*用户组*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("UserGroups")]
    public class UserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("group_id")]
        public int GroupId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("group_name")]
        public required string GroupName { get; set; }

        [Required]
        [ForeignKey("Creator")]
        [Column("creator_id")]
        public int CreatorId { get; set; }
        public required User Creator { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
