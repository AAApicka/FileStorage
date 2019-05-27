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
        GetFileResult GetFileByDId(GetFileRequest getFileRequest);
        bool FileIdExists(int fileId);
        bool DocumentIdExists(int documentId);
        void RollBack();
    }
}
