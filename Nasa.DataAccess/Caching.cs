using System;
using System.Runtime.Caching;

namespace Nasa.DataAccess
{
    public class Caching
    {
        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="cacheItemName">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long to cache this object for.</param>
        /// <param name="objectSettingFunction">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        public static T GetObjectFromCache<T>(string index, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            var cache = MemoryCache.Default;
            var key = index;

            var result = (T)cache[index];
            if (result == null)
            {
                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);

                result = objectSettingFunction();

                cache.Set(index, result, policy);
            }

            return result;
        }

        public static T GetObjectFromCache<T>(string index, int cacheTimeInMinutes, string param1, Func<string, T> objectSettingFunction)
        {
            var cache = MemoryCache.Default;
            var key = index;

            var result = (T)cache[index];
            if (result == null)
            {
                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);

                result = objectSettingFunction(param1);

                cache.Set(index, result, policy);
            }

            return result;
        }




        //public static T GetObjectFromCache<T>(string container, string folder, int cacheTimeInMinutes, Func<string, string, T> objectSettingFunction)
        //{
        //    var cache = MemoryCache.Default;
        //    var key = $"{container}/{folder}";

        //    var result = (T)cache[key];
        //    if (result == null)
        //    {
        //        var policy = new CacheItemPolicy();
        //        policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);

        //        result = objectSettingFunction(container, folder);

        //        cache.Set(key, result, policy);
        //    }
        //    return result;
        //}
    }
}