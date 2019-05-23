using Elinkx.FileStorage.ServiceLayer;
using Elinkx.FileStorage.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Elinkx.FileStorage.DataLayer;

namespace Elinkx.FileStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IServiceLayer _service;

        public FileController(IServiceLayer service)
        {
            _service = service;
        }

        /// <summary>
        /// Service method call. Inserts new entry into the database or adds new version
        /// </summary>
        /// <param name="insertRequest">Required contract specification</param>
        /// <returns>Result of operation</returns>
        [Route("insert")]
        public InsertResult Insert(InsertRequest insertRequest)
        {
            return _service.Insert(insertRequest);
        }

        /// <summary>
        /// Service method call. Updates entry in the database
        /// </summary>
        /// <param name="updateRequest">Required contract specification</param>
        /// <returns>Result of operation</returns>
        [Route("update")]
        public UpdateResult Update(UpdateRequest updateRequest)
        {
            return _service.Update(updateRequest);
        }

        [Route("delete")]
        public DeleteResult Delete(DeleteRequest deleteRequest)
        {
            return _service.Delete(deleteRequest);
        }

        [Route("getmetadata")]
        public IEnumerable<GetMetadataResult> GetMetadata(GetMetadataRequest getMetadataRequest)
        {
            return _service.GetMetadata(getMetadataRequest);
        }

        [Route("getfile")]
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            return _service.GetFile(getFileRequest);
        }
    }

}
