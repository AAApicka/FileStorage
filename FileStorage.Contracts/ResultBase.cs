using System;
using System.Collections.Generic;
using System.Text;

namespace Elinkx.FileStorage.Contracts
{
    public abstract class ResultBase
    {
        public ResultTypes ResultType { get ; set; }
    }
    public enum ResultTypes
    {
        Inserted, Updated, Received, Deleted, NotInserted, NotUpdated, NotReceived, NotDeleted, Warning
    }
}
