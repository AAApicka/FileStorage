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
        Inserted, Updated, DataMissing, Deleted, NotInserted, NotUpdated, DataOk, NotDeleted, Warning, Received, NotReceived
    }
}
