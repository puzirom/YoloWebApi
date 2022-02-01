using Microsoft.AspNetCore.Mvc;
using YoloWebApi.Processor;

namespace YoloWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringReverseController : ControllerBase
    {
        private const string Text =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
            "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip " +
            "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore " +
            "eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " +
            "deserunt mollit anim id est laborum.";

        [HttpGet( "first")]
        public string Get()
        {
            return StringReverseProcessor.StringReverseFirst(Text);
        }

        [HttpGet("second")]
        public string GetSecond()
        {
            return StringReverseProcessor.StringReverseFirst(Text);
        }
    }
}
