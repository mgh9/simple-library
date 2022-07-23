using System;
using System.Linq;

namespace FinLib.Common.Helpers.Security
{
    public static class PasswordGenerator
    {
        private const string UPPER_CASE_CHARS = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
        private const string LOWER_CASE_CHARS = "abcdefghjkmnpqursuvwxyz";
        private const string NUMBERS_CHARS = "123456789";
        private const string SPECIALS_CHARS = "@$%&*#";

        private static readonly System.Random _random = new();

        public static string Generate(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
        {
            if (passwordSize <= 0 || passwordSize > 50)
            {
                throw new Exceptions.Business.BusinessValidationException("طول رمز عبور باید بین یک و 50 کرکتر باشد");
            }

            char[] password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            int counter;

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE_CHARS;

            if (useUppercase) charSet += UPPER_CASE_CHARS;

            if (useNumbers) charSet += NUMBERS_CHARS;

            if (useSpecial) charSet += SPECIALS_CHARS;

            for (counter = 0; counter < passwordSize; counter++)
            {
                password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            var generatedPassword = String.Join(null, password);
            if (useUppercase)
            {
                generatedPassword = makeSureUseThis(generatedPassword, UPPER_CASE_CHARS);
            }

            if (useLowercase)
            {
                generatedPassword = makeSureUseThis(generatedPassword, LOWER_CASE_CHARS);
            }

            if (useNumbers)
            {
                generatedPassword = makeSureUseThis(generatedPassword, NUMBERS_CHARS);
            }

            if (useSpecial)
            {
                generatedPassword = makeSureUseThis(generatedPassword, SPECIALS_CHARS);
            }

            return generatedPassword;
        }

        private static string makeSureUseThis(string password, string charset)
        {
            var isCharsetUsedInPassword = charset.Any(x => password.Contains(x));
            if (isCharsetUsedInPassword)
                return password;

            var indexToInsertCharsetChar = _random.Next(0, password.Length);
            var indexOfCharsetChar = _random.Next(0, charset.Length);

            password = password.Insert(indexToInsertCharsetChar, charset[indexOfCharsetChar].ToString());

            return password;
        }
    }
}
