using System;

namespace Elinkx.FileStorage.Contracts
{
    public class InsertVersionResult : ResultBase
    {
        public int RowId { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public bool Signed { get; set; }
    }

}
