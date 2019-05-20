using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts 
    {
    public class RejectResult 
        {
        public int FileId { get; set; }
        public bool Reject { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
    }

}
