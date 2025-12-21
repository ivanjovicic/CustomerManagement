using System.Collections;
using System.Web;

namespace CustomerManagement.WebApp
{
    public static class CacheHelper
    {
        public static void ClearCustomersCache()
        {
            var cache = HttpContext.Current?.Cache;
            if (cache == null)
            {
                return;
            }

            foreach (DictionaryEntry item in cache)
            {
                if (item.Key.ToString().StartsWith("customers_"))
                {
                    cache.Remove(item.Key.ToString());
                }
            }
        }
    }
}
