namespace YoloWebApi.Processor
{
    internal static class StringReverseProcessor
    {
        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            var i = -1;
            var j = s.Length;
            while (++i < --j)
            {
                charArray[i] ^= charArray[j];
                charArray[j] ^= charArray[i];
                charArray[i] ^= charArray[j];
            }
            return new string(charArray);
        }

        //public static string Reverse(string s)
        //{
        //    return new string(s.Reverse().ToArray());
        //}
    }
}
