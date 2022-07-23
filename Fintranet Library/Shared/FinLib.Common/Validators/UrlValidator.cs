namespace FinLib.Common.Validators
{
    public static class UrlValidator
    {
        public static bool IsValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) 
                                && 
                                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
