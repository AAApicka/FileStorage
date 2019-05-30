using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elinkx.FileStorage.DataLayer.Entities
{
    [Table("FileContent")]
    public class FileContent
    {
        [Key]
        public int RowId { get; set; }
        [Required]
        public byte[] Content { get; set; }

        public FileVersion FileVersion { get; set; }
    }
}
