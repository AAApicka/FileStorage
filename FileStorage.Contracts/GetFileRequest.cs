using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class GetFileRequest
    {
        public int FileId { get; set; }
        public int DocumentId { get; set; }
    }
}
