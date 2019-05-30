using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elinkx.FileStorage.DataLayer.Entities
{
    [Table("FileVersion")]
    public class FileVersion
    {
        [Key]
        public int VersionId { get; set; }
        [Required]
        public DateTime Changed { get; set; }
        [MaxLength(20), Required]
        public string ChangedBy { get; set; }
        [Required]
        public int Size { get; set; }
        public bool Signed { get; set; } = false;
        [ForeignKey("FileId")]
        public Metadata Metadata { get; set; }
        [ForeignKey("RowId")]
        public FileContent FileContent { get; set; }


        
    }
}
