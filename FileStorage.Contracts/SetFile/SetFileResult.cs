using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class SetFileResult
    {
        public int FileId { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
    }

}
