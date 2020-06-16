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
        [Key][Required]
        public string Name { get; set; }
    }
}
