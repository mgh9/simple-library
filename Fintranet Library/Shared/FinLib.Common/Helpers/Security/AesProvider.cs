using FinLib.Common.Extensions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FinLib.Common.Helpers.Security
{
    public static class AesProvider
    {
        private const string _defaultKey = "rg_idp_mg_hha#$%HHdbCVhjj&%AdsGH";
        private const string _defaultInitVector = "Fh3G%_@$atyb4jdu";

        public static string Encrypt(string textToCrypt)
        {
            return Encrypt(textToCrypt, _defaultKey, _defaultInitVector);
        }

        public static string Encrypt(string textToCrypt, string key, string initVector)
        {
            if(textToCrypt is null)
            {
                return null;
            }
            else if(textToCrypt.IsEmpty())
            {
                return "";
            }

            try
            {
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var initVectorBytes = Encoding.ASCII.GetBytes(initVector);

                using (var rijndaelManaged = new RijndaelManaged { Key = keyBytes, IV = initVectorBytes, Mode = CipherMode.CBC })
                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(keyBytes, initVectorBytes), CryptoStreamMode.Write))
                {
                    using (var ws = new StreamWriter(cryptoStream))
                    {
                        ws.Write(textToCrypt);
                    }

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            catch (CryptographicException ex)
            {
                throw new Exceptions.Infra.FatalException("خطا در رمزنگاری", ex);
            }
        }

        public static string Decrypt(string cipherData)
        {
            if (cipherData is null)
            {
                return null;
            }
            else if(cipherData.IsEmpty())
            {
                return "";
            }

            return Decrypt(cipherData, _defaultKey, _defaultInitVector);
        }

        public static string Decrypt(string cipherData, string key, string initVector)
        {
            try
            {
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var initVectorBytes = Encoding.ASCII.GetBytes(initVector);

                using (var rijndaelManaged = new RijndaelManaged { Key = keyBytes, IV = initVectorBytes, Mode = CipherMode.CBC })
                using (var memoryStream = new MemoryStream(Convert.FromBase64String(cipherData)))
                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(keyBytes, initVectorBytes), CryptoStreamMode.Read))
                {
                    return new StreamReader(cryptoStream).ReadToEnd();
                }
            }
            catch (CryptographicException ex)
            {
                throw new Exceptions.Infra.FatalException("خطا در رمزگشایی", ex);
            }
        }
    }
}
