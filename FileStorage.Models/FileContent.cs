using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elinkx.FileStorage.Models
{
    [Table("FileContent")]
    public class FileContent
    {
        [Key]
        public int RowId { get; set; }
        [Column("FileContent")]
        public byte[] Content { get; set; }
    }
}
