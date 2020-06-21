using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CherryBlossom.Persistent
{
    [Table("Message")]
    public class MessageModel
    {
        [Key]
        [Required]
        public string MessageId { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }

    }
}
