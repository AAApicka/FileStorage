 using System;

namespace Elinkx.FileStorage.Contracts {
    public class FileMetadata {
        public int FileId { get; set; }
        public string ContentType { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public int TypeId { get; set; }
        public int SubtypeId { get; set; }
        public bool Signed { get; set; }
        public string UserCode { get; set; }
    }
    public class FileReject {
        //public bool Reject { get; set; } bude resit service layer
        public string UserCode { get; set; }
        public int FileId { get; set; }
    }

}
