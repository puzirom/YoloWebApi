using System.Linq;
using System.Threading.Tasks;

namespace YoloWebApi.Processor
{
    internal static class ParallelWorkProcessor
    {
        public static async Task MethodA()
        {
            var valueArray = Enumerable.Range(1, 1000).ToArray();
            await Task.Run(() => Parallel.ForEach(valueArray, item => { MethodB(item); }));
        }

        private static bool MethodB(int value)
        {
            Task.Delay(100).Wait();
            return value > 0;
        }
    }
}
