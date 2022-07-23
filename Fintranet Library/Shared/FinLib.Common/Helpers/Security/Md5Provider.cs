using FinLib.Common.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace FinLib.Common.Helpers.Security
{
    [Obsolete("Use SHA256 or SHA512 algorithms [security concern]")]
    public static class Md5Provider
    {
        private const string _defaultSalt = "_m58-R0$8|)I5440E__f#65GF7A7%sd#";

        public static string Hash(string input, string salt = null)
        {
            salt ??= _defaultSalt;
        
            if (!salt.IsEmpty())
            {
                input = string.Concat(input , salt);
            }

            // Use input string to calculate MD5 hash
            using (var md5Hasher = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5Hasher.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
