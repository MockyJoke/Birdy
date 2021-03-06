using System.Security.Cryptography;
using System.Text;

namespace Birdy.Util
{
    public interface IHashGenerator
    {
        string GenerateHash(byte[] bytes, int length = 0);
    }
    public class HashGenerator : IHashGenerator
    {
        public string GenerateHash(byte[] bytes, int length = 0)
        {
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }
            if (length > 0)
            {
                return hashString.Substring(0, length);
            }
            return hashString;
        }

        public string SHA256(string text, int length = 0)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return GenerateHash(bytes, length);
        }
    }
}