using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class GetMetadataResult
    {
       public int FileId { get; set; }
        public string ContentType { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public string TypeId { get; set; }
        public string SubtypeId { get; set; }
        public bool Signed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ChangedBy { get; set; }
        public DateTime Changed { get; set; }
    }

}
