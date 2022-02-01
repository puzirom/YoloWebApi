using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoloWebApi.Processor;

namespace YoloWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParallelWorkController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await ParallelWorkProcessor.MethodA();
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            var elapsedTime = $"Result time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            return elapsedTime;
        }
    }
}
