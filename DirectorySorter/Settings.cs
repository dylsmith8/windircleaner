using System.Configuration;

namespace DirectorySorter
{
    public static class Settings
    {
        public static string FileWhiteList => ConfigurationSettings.AppSettings[@"FileTypesWhitelist"];
        public static string DirectoryToScan => ConfigurationSettings.AppSettings[@"DirectoryToScan"];
        public static int PollingInterval => int.Parse(ConfigurationSettings.AppSettings[@"PollingInterval"]);
    }
}
