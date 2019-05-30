using System;
using System.Collections.Generic;
using System.Text;

namespace Elinkx.FileStorage.Contracts
{
    public abstract class ResultBase
    {
        public ResultTypes ResultType { get ; set; }
        public string Message { get; set; }
    }
    public enum ResultTypes
    {
        Inserted, Updated, Deleted, GetMetadataSuccess, GetFileSuccess, Success,
        NotInserted, NotUpdated, NotDeleted, GetMetadataFailed, GetFileFailed, Failed
    }
}
