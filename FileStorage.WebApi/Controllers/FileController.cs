using Elinkx.FileStorage.ServiceLayer;
using Elinkx.FileStorage.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Elinkx.FileStorage.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FileController: ControllerBase {
        private readonly Service service;

        public FileController() {
            service = new Service();
        }
        [Route("setfile")]
        public SetFileResult SetFile(SetFileRequest setFileRequest) {
            return service.SetFile(setFileRequest);
        }

        [Route("Rejectfile")]
        public RejectResult SetReject(SetRejectRequest setRejectRequest) {
            return service.SetReject(setRejectRequest);

        }
        [Route("GetMetadata")]
        public List<GetMetadataResult> ReadMetadata(GetMetadata readMetadata) {
            return service.ReadMetadata(readMetadata);

        }
        [Route("GetFile")]
        public GetContentResult GetFile(GetContentRequest getFile) {
             return service.GetContent(getFile);

         }
    }
}
