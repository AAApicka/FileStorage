using Elinkx.FileStorage.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elinkx.FileStorage.DataLayer
{
    public interface IData
    {
        SetFileResult SetFile(SetFileRequest setFileRequest);
        SetRejectResult SetReject(SetRejectRequest setRejectRequest);
        List<GetMetadataResult> GetMetadataByDate(GetMetadataByDateRequest getMetadataByDateRequest);
        GetFileResult GetFile(GetFileRequest getFileRequest);
    }
}
