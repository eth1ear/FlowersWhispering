/*文章和公告*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Articles")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("article_id")]
        public int ArticleId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("article_title")]
        public required string ArticleTitle { get; set; }

        [Required]
        [Column("article_content",TypeName = "CLOB")]
        public required string ArticleContent { get; set; }

        [ForeignKey("Publisher")]
        [Column("publisher_id")]
        public int? PublisherId { get; set; } //匿名发布时为null
        public required User Publisher { get; set; }

        [Column("published_date")]
        public DateTime PublishedDate { get; set; }

        [Required]
        [StringLength(50)]
        public required string ArticleType { get; set; }
    }
}
