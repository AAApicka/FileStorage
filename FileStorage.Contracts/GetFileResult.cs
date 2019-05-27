using System;
using System.ComponentModel.DataAnnotations;

namespace Elinkx.FileStorage.Contracts
{
    public class GetFileResult: ResultBase
    {
        public byte[] Content { get; set; }
    }
}
