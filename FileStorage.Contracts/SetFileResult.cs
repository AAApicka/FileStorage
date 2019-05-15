using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class SetFileResult
    {
        public string UserCode { get; set; }
        public int FileId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
    }

}
