using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using YoloWebApi.Contracts;

namespace YoloWebApi.Processor
{
    internal static class AssetMarketProcessor
    {
        
        private static readonly GraphQLHttpClient Client = new GraphQLHttpClient(WebApiUrl, new NewtonsoftJsonSerializer());

        public static async Task<IEnumerable<Market>> GetPriceForTopHundred()
        {
            var assetArray = await Task.Run(GetTopHundredAssets);
            var itemsCount = assetArray.Length;
            if (itemsCount < 1)
            {
                return Array.Empty<Market>();
            }

            const int normalBatchLimit = 20;
            var lastBatchLimit = itemsCount % normalBatchLimit;
            var batchesCount = lastBatchLimit > 0 ? itemsCount / normalBatchLimit + 1 : itemsCount / normalBatchLimit;
            var resultArray = new List<Market>[batchesCount];

            //splitting assets into batches
            for(var i = 0; i < batchesCount; i++)
            {
                //defining a subset of documents according to limit
                var length = i + 1 == batchesCount && lastBatchLimit > 0 ? lastBatchLimit : normalBatchLimit;
                var subsetArray = new Asset[length];
                Array.Copy(assetArray, i * normalBatchLimit, subsetArray, 0, length);

                //get batch markets for subset of assets
                var markets = await GetBatchMarkets(subsetArray);
                resultArray[i] = new List<Market>();
                resultArray[i].AddRange(markets);
            }

            //join batches results into one enumerable instance
            return resultArray.SelectMany(x => x);
        }

        #region graphql

        private static async Task<Asset[]> GetTopHundredAssets()
        {
            //query takes only first 100 items
            const string query = @"
                query {
                  assets(sort: [{marketCapRank: ASC}], page: {skip: 0, limit: 100}) {
                    assetName
                    assetSymbol
                    marketCap
                  }
                }";

            var msg = new GraphQLRequest
            {
                Query = query
            };

            var assetsResponse = await Client.SendQueryAsync<DataAssets>(msg);
            return assetsResponse.Data.Assets;
        }

        private static async Task<Market[]> GetBatchMarkets(IEnumerable<Asset> assets)
        {
            var assetSymbols = assets.Select(a => $"\"{a.AssetSymbol}\"");
            var sb = string.Join(",", assetSymbols);

            var query = 
                "query { " +
                "  markets(filter: { baseSymbol: { _in:[" + sb + "]}, quoteSymbol: { _eq: \"EUR\"}, exchangeSymbol: { _eq: \"Binance\"} }) {" +
                "    marketSymbol " +
                "    ticker { lastPrice } " +
                "  } " +
                "}";

            var msg = new GraphQLRequest
            {
                Query = query
            };

            var response = await Client.SendQueryAsync<DataMarkets>(msg);
            return response.Data.Markets;
        }

        private const string WebApiUrl = @"https://api.blocktap.io/graphql";

        #endregion
    }
}
