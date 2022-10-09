using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache//öncelikle cache nesnesi oluşturuyoruz.
        {
            get { return MemoryCache.Default; }  
        }
        public void Add(string key, object data, int cacheTime=60)
        {
            if (data==null)//gelen datayı kontrol ediyoruz,eğer cacheleyeceğimiz data yoksa 
            {
                return;
            }//boş değilse cache'e ekleyeceğiz.
            //AbsoluteExpiration : cache'de ne kadar dursun ,
            //şuandan itibaren gönderdiğimi cacheTime kadar cache'de tut
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };//şuandan itibaren 60 dk kadar cache'de dursun
            Cache.Add(new CacheItem(key,data),policy);//gönderdiğim policy ile, gönderdiğim key ile datayı cacheleyecek.
        }

        public void Clear()
        {
            foreach (var item in Cache)//bütün cacheleri sil
            {
                MemoryCache.Default.Remove(item.Key);
            }
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];//cache datalarından,gönderdiğimiz key isminde olan cache'i çek, T'ye dönüştür ve ver 
        }

        public bool IsAdd(string key)//cache'de var mı?
        {
            return Cache.Contains(key);//cache ile gönderdiğim data cache'de var mı?
        }

        public void Remove(string key)
        {
            Cache.Remove(key);//key değerini cache'den siliyoruz.
        }

        public void RemoveByPattern(string pattern)
        {
            //Singleline : teksatır ,Compiled : compile edilmiş ,IgnoreCase : case sensitive çalışma
            var regex =new Regex(pattern, RegexOptions.Singleline|RegexOptions.Compiled|RegexOptions.IgnoreCase);
            //bu regex'e göre hangi cache'leri sileceğiz ,sileceğimiz cacheler;
            //regex'den match olanları,key datasına göre çek,ve listeye çevirip keysToRemove'a at
            var keysToRemove = Cache.Where(d => regex.IsMatch(d.Key)).Select(d => d.Key).ToList();
            //bütün cacheleri gezip bu key'e sahip olanları bellekten sileceğiz
            foreach (var key in keysToRemove)
            {
                MemoryCache.Default.Remove(key);
            }
            
        }
    }
}
