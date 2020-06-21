using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CherryBlossom.Persistent
{
    [Table("Morph")]
    public class MorphModel
    {
        [Key]
        [Required]
        public string Morph { get; set; }

        public string Alpha { get; set; }

    }
}
