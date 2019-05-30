using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elinkx.FileStorage.DataLayer.Entities
{
    [Table("Metadata")]
    public class Metadata
    {
        [Key]
        public int FileId { get; set; }
        [MaxLength(150), Required]
        public string ContentType { get; set; }
        public int SubjectId { get; set; }
        [MaxLength(255), Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int DocumentId { get; set; }
        [MaxLength(20), Required]
        public string TypeId { get; set; }
        [MaxLength(20)]
        public string SubtypeId { get; set; }
        [Required]
        public bool Reject { get; set; } = false;
        [MaxLength(20), Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [MaxLength(20), Required]
        public string ChangedBy { get; set; }
        [Required]
        public DateTime Changed { get; set; }

        public List<FileVersion> FileVersion { get; set; }
    }
}
