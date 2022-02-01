using Newtonsoft.Json;

namespace YoloWebApi.Contracts
{
    public class DataAssets
    {
        [JsonProperty("assets")]
        public Asset[] Assets { get; set; }
    }

    public class Asset
    {
        [JsonProperty("assetName")]
        public string AssetName { get; set; }

        [JsonProperty("assetSymbol")]
        public string AssetSymbol { get; set; }

        [JsonProperty("marketCap")]
        public long? MarketCap { get; set; }
    }
}
