using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoloWebApi.Contracts;
using YoloWebApi.Processor;

namespace YoloWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetMarketController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Market>> Get()
        {
            var result = await AssetMarketProcessor.GetPriceForTopHundred();
            return result;
        }
    }
}
