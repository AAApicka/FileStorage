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
        /// Inserts
        /// </summary>
        /// <param name="insertRequest">Required contract specification</param>
        /// <returns>Result of operation</returns>
        [Route("insert")]
        public InsertResult Insert(InsertRequest insertRequest)
        {
            return _service.Insert(insertRequest);
        }

        [Route("insertversion")]
        public InsertVersionResult InsertVersion(InsertVersionRequest insertVersionRequest)
        {
            return _service.InsertVersion(insertVersionRequest);
        }

        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="updateRequest">Required contract specification</param>
        /// <returns>Result of operation</returns>
        [Route("update")]
        public UpdateResult Update(UpdateRequest updateRequest)
        {
            return _service.Update(updateRequest);
        }

        /// <summary>
        /// Deletes
        /// </summary>
        /// <param name="deleteRequest"></param>
        /// <returns></returns>
        [Route("delete")]
        public DeleteResult Delete(DeleteRequest deleteRequest)
        {
            return _service.Delete(deleteRequest);
        }

        /// <summary>
        /// Gets metadata
        /// </summary>
        /// <param name="getMetadataRequest"></param>
        /// <returns></returns>
        [Route("getmetadata")]
        public IEnumerable<GetMetadataResult> GetMetadata(GetMetadataRequest getMetadataRequest)
        {
            return _service.GetMetadata(getMetadataRequest);
        }

        /// <summary>
        /// Gets content 
        /// </summary>
        /// <param name="getFileRequest"></param>
        /// <returns><see cref="GetFileResult"/></returns>
        [Route("getfile")]
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            return _service.GetFile(getFileRequest);
        }
    }

}
