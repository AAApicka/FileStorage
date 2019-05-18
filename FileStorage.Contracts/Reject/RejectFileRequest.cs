using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts 
    {
    public class SetRejectRequest 
        {
        public int FileId { get; set; }
        public string UserCode { get; set; }
        public bool Rejected { get; set; }


    }
}
