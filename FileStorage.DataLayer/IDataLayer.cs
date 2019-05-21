using Elinkx.FileStorage.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elinkx.FileStorage.DataLayer
{
    public interface IDataLayer
    {
        SetFileResult SetFile(SetFileRequest setFileRequest);
        SetRejectResult Delete(SetRejectRequest setRejectRequest);
        List<GetMetadataResult> GetMetadataByDate(GetMetadataByDateRequest getMetadataByDateRequest);
        GetFileResult GetFile(GetFileRequest getFileRequest);
    }
}
