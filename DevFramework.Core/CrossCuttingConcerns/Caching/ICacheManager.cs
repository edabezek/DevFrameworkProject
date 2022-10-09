using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);  //vereceğimiz keye göre cache datasını getir 
        void Add(string key,object data,int cacheTime);//cacheTime:data cache'de ne kadar kalacak
        bool IsAdd(string key);//daha önce eklenmiş böyle bir cache datası var mı,ona göre ya db'den yada cache'den getireceğiz.  
        void Remove(string key);//cache silmemiz gerekecek 
        void RemoveByPattern(string pattern);//bazen bir pattern'e göre silmek isteyeceğiz
        void Clear();//cache'i tamamen silmek istersek 
    }
}
