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
        FileMetadata fmTest = new FileMetadata();

        public FileController() {
            service = new Service();
        }
        
        public ActionResult SetFile() {
            fmTest.ContentType = "ctype";
            fmTest.Description = "tdstd";
            fmTest.Name = "name";
            fmTest.FileId = 1;
            service.SetFile(fmTest);
            return Content("done");
        }
    }
    
}
