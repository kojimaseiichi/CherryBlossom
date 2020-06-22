using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CherryBlossom.Persistent
{
    [Table("Item")]
    public class ItemModel
    {
        [Key]
        [Required]
        public string ItemExpression { get; set; }

        [Required]
        public string ItemAlpha { get; set; }

        [Required]
        public string DataType { get; set; }

        public string Length { get; set; }

        public string Precision { get; set; }

        public string Scale { get; set; }

        public string RangeMin { get; set; }

        public string RangeMax { get; set; }

        public string RegularExpression { get; set; }
    }
}
