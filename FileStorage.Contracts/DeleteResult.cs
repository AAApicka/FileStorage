using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class DeleteResult : ResultBase
    {
        public int FileId { get; set; }
    }

}
