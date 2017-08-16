using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Northwind.MvcWebUI.ExtensionMethods
{
    public static class SessionExtensionMethods
    {
        public static void SetObject(this ISession session,string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key,objectString);
        }

        public static T GetObject<T>(this ISession session, string key) where T:class //herhangi bir obje olabilir o yüzden t kullandık
        {
            string objetcString = session.GetString(key);
            if (string.IsNullOrEmpty(objetcString))
            {
                return null;
            }
            else
            {
                T value = JsonConvert.DeserializeObject<T>(objetcString);
                return value;
            }
        }
    }
}
