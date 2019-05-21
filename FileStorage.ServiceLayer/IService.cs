using System;
using System.Collections.Generic;
using System.Text;
using Elinkx.FileStorage.Contracts;

namespace Elinkx.FileStorage.ServiceLayer
{
   public interface IService
    {
        List<GetMetadataResult> GetMetadataByDate(GetMetadataByDateRequest getMetadataByDateRequest);
        GetFileResult GetFile(GetFileRequest getFileRequest);
        SetRejectResult Delete(SetRejectRequest setRejectRequest);
        SetFileResult SetFile(SetFileRequest fileMetadata);
    }
}
