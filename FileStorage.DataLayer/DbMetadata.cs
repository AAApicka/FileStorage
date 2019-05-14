using System;
using System.Collections.Generic;
using System.Text;

namespace Elinkx.FileStorage.DataLayer {
    public class DbMetadata {
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
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
