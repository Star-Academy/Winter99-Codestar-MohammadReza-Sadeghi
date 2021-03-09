using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Phase04
{
    class Parser
    {
        public static List<T> jsonToList<T>(string JsonStr)
        {
            return JsonConvert.DeserializeObject<List<T>>(JsonStr);
        }
    }
}
