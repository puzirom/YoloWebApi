using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YoloWebApi.Processor
{
    internal static class HashComputeProcessor
    {
        public static async Task<string> GetHashSha256(string filename)
        {
            return await Task.Run(() => GetHash(filename));
        }

        private static string GetHash(string filename)
        {
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(filename);
            var array = sha256.ComputeHash(stream);
            var result = new StringBuilder();
            foreach (var item in array)
            {
                result.Append($"{item:X2}");
            }
            return result.ToString();
        }
    }
}
