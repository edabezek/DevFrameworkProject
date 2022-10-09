﻿using DevFramework.Core.CrossCuttingConcerns.Caching;
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
    public class CacheAspect : MethodInterceptionAspect
    {
        //cache'i yaparken hangi cache'i kullanacağımızı belirteceğiz
        private Type _cacheType;
        //cache'de ne kadar duraxcak
        private int _cacheByMinute;
        //hangi cache'i kullanacaksak onu atayacağız.
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheByMinute=60)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
        }
        //hangi cache managerı gönderdiyse onu initialize etmemiz gerekiyor
        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType)==false)//gönderilen cache type, bir cache manager türünde değilse
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager=(ICacheManager)Activator.CreateInstance(_cacheType);//gönderilen cache type için bir instance ürettik

            base.RuntimeInitialize(method);
        }
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}",args.Method.ReflectedType.Namespace,args.Method.ReflectedType.Name,args.Method.Name);

            var arguments = args.Arguments.ToList();

            var key = string.Format("{0}.({1})", methodName, string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));
            if (_cacheManager.IsAdd(key))
            {
                args.ReturnValue=_cacheManager.Get<object>(key);    
            }
            base.OnInvoke(args);
            _cacheManager.Add(key, args.ReturnValue, _cacheByMinute);
        }
    }
}
