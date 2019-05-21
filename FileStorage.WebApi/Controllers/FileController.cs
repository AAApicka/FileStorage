using Elinkx.FileStorage.ServiceLayer;
using Elinkx.FileStorage.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Elinkx.FileStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly Service service;

        public FileController()
        {
            service = new Service();
        } 
        [Route("setfile")]
        public SetFileResult SetFile(SetFileRequest setFileRequest)
        {
            return service.SetFile(setFileRequest);
        }
        [Route("setreject")]
        public SetRejectResult Delete(SetRejectRequest setRejectRequest)
        {
            return service.Delete(setRejectRequest);
        }
        [Route("getmetadata")]
        public List<GetMetadataResult> GetMetadataByDate(GetMetadataByDateRequest getMetadataByDateRequest)
        {
            return service.GetMetadataByDate(getMetadataByDateRequest);
        }
        [Route("getfile")]
        public GetFileResult GetFile(GetFileRequest getFileRequest)
        {
            return service.GetFile(getFileRequest);
        }
    }

}
