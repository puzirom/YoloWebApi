using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoloWebApi.Processor;

namespace YoloWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HashComputeController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return "Filename is not provided";
            }

            if (!System.IO.File.Exists(filename))
            {
                return "File does not exist";
            }

            var result = await HashComputeProcessor.GetHashSha256(filename);
            return result;
        }
    }
}
