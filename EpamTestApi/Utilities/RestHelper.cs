using EpamTestApi.Config;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTestApi.Utilities
{
    public class RestHelper
    {
        private RestClient _client;
        public RestClient RestClient
        {
            get
            {
                if (_client == null)
                {
                    CreateRestClient();
                }
                return _client;
            }
        }

        private void CreateRestClient()
        {
            RestClientOptions restClientOptions = new RestClientOptions(Configuration.AppSetting.BaseURL);
            _client = new RestClient(restClientOptions);
        }
    }
}
