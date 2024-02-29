using Application.Storage.Commands.Upload;
using Application.Storage.Commands.Vision;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstract;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ApiControllerBase
    {
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFileAsync(UploadCommand command)
        {
            return await GuideActionAsync(command);
        }

        [HttpPost("Vision")]
        public async Task<IActionResult> VisionAsync(VisionCommand command)
        {
            return await GuideActionAsync(command);
        }
    }
}
