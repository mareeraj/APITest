using Microsoft.Extensions.Configuration;

namespace EpamTestApi.Config
{
    public class Configuration
    {
        private static AppSetting _config;
        public static AppSetting AppSetting
        {
            get { return _config; }
        }

        public static void BuildAppConfiguration()
        {
            if (_config == null)
            {
                _config = new AppSetting();
                new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile(@"./Config/AppSetting.json")
                   .Build()
                   .Bind(_config);
            }
        }
    }
}
