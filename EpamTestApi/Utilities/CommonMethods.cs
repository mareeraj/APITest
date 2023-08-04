using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EpamTestApi.Utilities
{
    public static class CommonMethods
    {
        public static T GetTestData<T>()
        {
            T data = default;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $@"TestData\PostInfo.json");
            if (!string.IsNullOrEmpty(path))
            {
                string jsonString = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<T>(jsonString);
            }
            return data;
        }
    }
}
