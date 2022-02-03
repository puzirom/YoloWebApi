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
        private const string WebApiUrl = @"https://api.blocktap.io/graphql";
        private static readonly GraphQLHttpClient Client = new GraphQLHttpClient(WebApiUrl, new NewtonsoftJsonSerializer());

        public static async Task<IEnumerable<Market>> GetPriceForTopHundred()
        {
            var topHundredAssets = await Task.Run(GetTopHundredAssets);
            var itemsCount = topHundredAssets.Length;
            if (itemsCount < 1)
            {
                return Array.Empty<Market>();
            }

            const int normalBatchLimit = 20;
            var lastBatchLimit = itemsCount % normalBatchLimit;
            var batchesCount = lastBatchLimit > 0 ? itemsCount / normalBatchLimit + 1 : itemsCount / normalBatchLimit;
            var resultArray = new List<Market>[batchesCount];

            //splitting assets into batches
            await Task.Run(() => Parallel.For(0, batchesCount, index =>
            {
                //defining a subset of documents according to limit
                var length = index + 1 == batchesCount && lastBatchLimit > 0 ? lastBatchLimit : normalBatchLimit;
                var subsetArray = new Asset[length];
                Array.Copy(topHundredAssets, index * normalBatchLimit, subsetArray, 0, length);
                var markets = GetBatchMarkets(subsetArray);
                resultArray[index] = new List<Market>();
                resultArray[index].AddRange(markets);
            }));

            //join batches results into one enumerable instance
            return resultArray.SelectMany(x => x);
        }

        private static Asset[] GetTopHundredAssets()
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

            var assetsResponse = Client.SendQueryAsync<DataAssets>(msg).Result;
            return assetsResponse.Data.Assets;
        }

        //private static Asset[] GetTopHundredAssets()
        //{
        //    //query takes all available items
        //    const string query = @"
        //        query {
        //          assets(sort: [{marketCapRank: ASC}]) {
        //            assetName
        //            assetSymbol
        //            marketCap
        //          }
        //        }";

        //    var msg = new GraphQLRequest
        //    {
        //        Query = query
        //    };

        //    var assetsResponse = Client.SendQueryAsync<DataAssets>(msg).Result;
        //    return assetsResponse.Data.Assets.Take(100).ToArray();
        //}

        private static IEnumerable<Market> GetBatchMarkets(IEnumerable<Asset> assets)
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

            var response = Client.SendQueryAsync<DataMarkets>(msg).Result;
            return response.Data.Markets;
        }
    }
}
