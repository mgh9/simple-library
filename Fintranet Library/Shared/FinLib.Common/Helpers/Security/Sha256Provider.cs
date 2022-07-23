using FinLib.Common.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace FinLib.Common.Helpers.Security
{
    public static class Sha256Provider
    {
        private const string _defaultSalt = "_m58-R0$8|)I5440E__f#65GF7A7%sd#";

        public static string Hash(string input, string salt = null)
        {
            salt ??= _defaultSalt;
            if (!salt.IsEmpty())
            {
                input = string.Concat(input, salt);
            }

            var sb = new StringBuilder();
            using (var sha256Hasher = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                byte[] result = sha256Hasher.ComputeHash(enc.GetBytes(input));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("X2"));
                }
            }

            return sb.ToString();
        }
    }
}
