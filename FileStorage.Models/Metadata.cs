using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elinkx.FileStorage.Models
{
    [Table("Metadata")]
    public class Metadata
    {
        [Key]
        public int FileId { get; set; }
        //vsechny stringy omezit na 20 znaku zde v modelu i v sql pomoci varchar20
        public string ContentType { get; set; } //pdf
        public int SubjectId { get; set; } // firma, ičo?
        public string Name { get; set; } // 
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public string TypeId { get; set; }// smlouva, faktura
        public string SubtypeId { get; set; }//jaka smlouva
        public bool Signed { get; set; }
        public bool Reject { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ChangedBy { get; set; }
        public DateTime Changed { get; set; }
    }
}
