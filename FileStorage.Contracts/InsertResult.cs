using System;

namespace Elinkx.FileStorage.Contracts
{
    public class InsertResult : ResultBase
    {
        public int FileId { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
    }

}
