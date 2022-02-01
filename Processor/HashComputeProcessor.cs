using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YoloWebApi.Processor
{
    internal static class HashComputeProcessor
    {
        public static string GetHashSha256(string filename)
        {
            filename = filename.Replace(@"\\", @"\");
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
