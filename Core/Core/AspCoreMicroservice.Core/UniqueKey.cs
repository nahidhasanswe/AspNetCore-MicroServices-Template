using System.Text;
using System.Security.Cryptography;

namespace AspCoreMicroservice.Core
{
    public class UniqueKey
    {
        private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";

        public static string Generate(int size=6)
        {
            char[] chars = Alphabet.ToCharArray();

            var crypto = new RNGCryptoServiceProvider();

            var data = new byte[size];
            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length-1)]);
            }

            return result.ToString();
        }
    }
}
