using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Elinkx.FileStorage.DataLayer {
    [Table("MetaData")]
    public class DbMetadata {
        [Key]
        public int FileId { get; set; }
        public string ContentType { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public int TypeId { get; set; }
        public int SubtypeId { get; set; }
        public bool Signed { get; set; }
        public string Reject { get; set; }
        [Column("CreatetBy")]
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
