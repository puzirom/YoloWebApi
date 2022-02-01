using Newtonsoft.Json;

namespace YoloWebApi.Contracts
{
    public class DataMarkets
    {
        [JsonProperty("markets")]
        public Market[] Markets { get; set; }
    }

    public class Market
    {
        [JsonProperty("marketSymbol")]
        public string MarketSymbol { get; set; }

        [JsonProperty("ticker")]
        public Ticker Ticker { get; set; }
    }

    public class Ticker
    {
        [JsonProperty("lastPrice")]
        public string LastPrice { get; set; }
    }
}
