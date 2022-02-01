using Microsoft.AspNetCore.Mvc;
using YoloWebApi.Processor;

namespace YoloWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HashComputeController : ControllerBase
    {
        [HttpGet]
        public string Get(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return "Filename is not provided";
            }

            if (!System.IO.File.Exists(filename))
            {
                return "File is not exist";
            }

            var result = HashComputeProcessor.GetHashSha256(filename);
            return result;
        }
    }
}
