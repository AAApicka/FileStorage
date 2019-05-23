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
        public string ContentType { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public string TypeId { get; set; }
        public string SubtypeId { get; set; }
        public bool Signed { get; set; }
        public bool Reject { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ChangedBy { get; set; }
        public DateTime Changed { get; set; }

        public List<FileVersion> FileVersion { get; set; }
    }
}
