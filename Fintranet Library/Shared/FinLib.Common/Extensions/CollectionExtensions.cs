using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace FinLib.Common.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            return collection.Cast<string>().ToDictionary(k => k, v => collection[v]);
        }
    }
}
