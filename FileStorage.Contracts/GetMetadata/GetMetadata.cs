using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class GetMetadata
    {
        public int FileId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Created2 { get; set; }
    }
}
