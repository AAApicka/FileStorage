using Elinkx.FileStorage.ServiceLayer;
using Elinkx.FileStorage.Contracts;
using Microsoft.AspNetCore.Mvc;

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
    }

}
