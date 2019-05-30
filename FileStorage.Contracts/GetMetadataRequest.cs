using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class GetMetadataRequest
    {   
        //search parameters set 1
        public int DocumentId { get; set; }
        public string TypeId { get; set; }
        //optional
        public string SubtypeId { get; set; }

        //search parameters set 2
        public int SubjectId { get; set; }
    }
}
