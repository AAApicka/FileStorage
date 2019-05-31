using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Elinkx.FileStorage.Contracts
{
    public class GetMetadataResult: ResultBase
    {
        public int FileId { get; set; }
        public string ContentType { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public string TypeId { get; set; }
        public string SubtypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ChangedBy { get; set; }
        public DateTime Changed { get; set; }
        //[NonSerialized]
        public FileVersionResult LastVersion { get { return AllVersions.OrderBy(c => c.Changed).Last();  } }
        public List<FileVersionResult> AllVersions { get; set; }
    }
    public class FileVersionResult
    {
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public int Size { get; set; }
        public bool Signed { get; set; }
        public int RowId { get; set; }
    }
}
