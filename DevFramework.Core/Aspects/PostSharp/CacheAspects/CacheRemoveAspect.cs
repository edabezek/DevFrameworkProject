using DevFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.CacheAspects
{
    [Serializable]
    public class CacheRemoveAspect : OnMethodBoundaryAspect
    {
        //manager'daki bütün cacheleri silmek yada bazılarını sil diyeceğiz
        private string _pattern;
        private Type _cacheType;
        ICacheManager _cacheManager;
        //sadece cachetype'ı isteyip ona göre silme yapacağız
        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }
        //string ve cachetype isteyeceğiz
        public CacheRemoveAspect(string pattern, Type cacheType)
        {
            _pattern = pattern;
            _cacheType = cacheType;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)//gönderilen cache type, bir cache manager türünde değilse
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);//gönderilen cache type için bir instance ürettik

            base.RuntimeInitialize(method);
        }
        public override void OnSuccess(MethodExecutionArgs args)
        {
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern) ? string.Format("{0}.{1}.*", args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name) : _pattern);
        }
    }
}
