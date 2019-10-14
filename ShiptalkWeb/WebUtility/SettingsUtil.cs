using System.Configuration;

namespace ShiptalkWeb.WebUtility
{
    public static class SettingsUtil
    {
        static SettingsUtil()
        {
            GoogleApiKey = ConfigurationManager.AppSettings["GoogleApiKey"];

            if (string.IsNullOrEmpty(GoogleApiKey))
                throw new ConfigurationErrorsException(
                    "Application Setting \"{0}\" is required for the application function correctly.");
        }

        public static string GoogleApiKey { get; private set; }
    }
}