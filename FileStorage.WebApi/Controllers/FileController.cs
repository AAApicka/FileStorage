using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elinkx.FileStorage.Contracts;
using Elinkx.FileStorage.ServiceLayer;
using System.Web;

namespace Elinkx.FileStorage.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase {
        private readonly Service service;
        
        public FileController() {
            service = new Service();
        }
        [HttpPost]
        public FileMetadata SetFile(FileMetadata fileMetadata) {
            return service.SetFile(fileMetadata);
        }
        



        [HttpPost]
        public FileMetadata GetFileMetadata() {
            return service.GetFileMetadata();
        }
        [HttpPost]
        public FileReject GetFileReject() {
            return service.GetFileReject();
        }
        [HttpPost]
        public FileReject SetFileReject() {
            return service.SetFileReject();
        }

        public ActionResult IsHealthy() {
            return Content("isHealthy");
        }
    }
    
}
