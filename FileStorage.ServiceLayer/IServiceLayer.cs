using System.Collections.Generic;
using Elinkx.FileStorage.Contracts;

namespace Elinkx.FileStorage.ServiceLayer
{
    public interface IServiceLayer
    {
        DeleteResult Delete(DeleteRequest deleteRequest);
        GetFileResult GetFile(GetFileRequest getFileRequest);
        IEnumerable<GetMetadataResult> GetMetadata(GetMetadataRequest getMetadataRequest);
        InsertResult Insert(InsertRequest insertRequest);
        UpdateResult Update(UpdateRequest updateRequest);
    }
}