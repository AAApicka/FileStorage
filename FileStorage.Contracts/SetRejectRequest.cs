using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class SetRejectRequest
    {
        public int FileId { get; set; }
        public bool Reject { get; set; }
        public string UserCode { get; set; }

    }
}
