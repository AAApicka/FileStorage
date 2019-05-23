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
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public int Size { get; set; }
        public int FileId { get; set; }
        public int RowId { get; set; }
        public Metadata Metadata { get; set; }
        public FileContent FileContent { get; set; }


        
    }
}
