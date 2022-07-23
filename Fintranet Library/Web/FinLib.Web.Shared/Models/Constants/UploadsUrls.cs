namespace FinLib.Web.Shared.Models.Constants
{
    public static class UploadsUrls
    {
        public const string _BaseUrl = "/uploads";

        public static class ClientLogo
        {
            public const string BaseUrl = _BaseUrl + "/client-logo";

            public const string DefaultLogoUrl = BaseUrl + "/default.png";
        }

        public static class UserImage
        {
            public const string BaseUrl = _BaseUrl + "/user-profile";

            public const string Default = BaseUrl + "/default.png";
        }
    }
}
