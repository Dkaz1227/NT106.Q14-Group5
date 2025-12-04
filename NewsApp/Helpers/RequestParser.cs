using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewsApp.Models
{
    public static class RequestParser
    {
        public static T Parse<T>(object data)
        {
            if (data == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(data.ToString());
        }
    }
}
