using Elinkx.FileStorage.Contracts;
using System.Collections.Generic;

namespace Elinkx.FileStorage.DataLayer
{
    public interface IDataLayer
    {
        InsertResult Insert(InsertRequest insertRequest);
        UpdateResult Update(UpdateRequest updateRequest);
        DeleteResult Delete(DeleteRequest deleteRequest);
        IEnumerable<GetMetadataResult> GetMetadata(GetMetadataRequest getMetadataRequest);
        GetFileResult GetFile(GetFileRequest getFileRequest);
        bool FileIdExists(int fileId);
        void RollBack();
    }
}
