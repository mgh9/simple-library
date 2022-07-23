using FinLib.Common.Exceptions.Business;

namespace FinLib.Common.Validators.FileUpload
{
    public static class FileContentValidator
    {
        private static HashSet<string> _forbiddenWords = new();

        public static void PopulateForbiddenWords(HashSet<string> forbiddenWords)
        {
            _forbiddenWords = forbiddenWords;
        }

        public static void Validate(string fileName, int maxAllowedForbiddenWordsInFileContent = 1)
        {
            var countOfForbiddenWords = 0;
            var content = File.ReadAllTextAsync(fileName).Result;

            foreach (var substringToSearch in _forbiddenWords)
            {
                var found = content.Contains(substringToSearch, StringComparison.OrdinalIgnoreCase);
                if (found)
                {
                    countOfForbiddenWords++;

                    if (countOfForbiddenWords > maxAllowedForbiddenWordsInFileContent)
                        break;
                }
            }

            if (countOfForbiddenWords >= maxAllowedForbiddenWordsInFileContent)
            {
                throw new FileUploadFailedException("فایل جهت بارگذاری نامعتبر می باشد. کد 1014");
            }
        }
    }
}
