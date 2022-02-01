using System.Collections.Generic;
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
        public IEnumerable<Market> Get()
        {
            var result = AssetMarketProcessor.GetPriceForTopHundred();
            return result;
        }
    }
}
