/*私信*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersWhisperingAPI.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("message_id")]
        public int MessageId { get; set; }

        [Required]
        [ForeignKey("Sender")]
        [Column("sender_id")]
        public int SenderId { get; set; }
        public required User Sender { get; set; }

        [Required]
        [ForeignKey("Receiver")]
        [Column("receiver_id")]
        public int ReceiverId { get; set; }
        public required User Receiver { get; set; }

        [Required]
        [Column("message_content",TypeName = "CLOB")]
        public required string MessageContent { get; set; }

        [Column("sent_date")]
        public DateTime SentDate { get; set; }
    }
}