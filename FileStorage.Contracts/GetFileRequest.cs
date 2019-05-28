using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class GetFileRequest
    {
        public string UserCode { get; set; }
        public int RowId { get; set; }
    }
}
