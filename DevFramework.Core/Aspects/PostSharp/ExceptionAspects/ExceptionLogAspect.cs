using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.ExceptionAspects
{
    [Serializable]
    public class ExceptionLogAspect : OnExceptionAspect
    {
        [NonSerialized]
        private readonly Type _loggerType;//db yada file 
        private LoggerService _loggerService;

        public ExceptionLogAspect(Type loggerType=null)
        {
            _loggerType = loggerType;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType!=null)//eğer logger type verilmemişse
            {
                if (_loggerType.BaseType != typeof(LoggerService))//veya loggertype bir loggerservice değilse ex fırlat
                {
                    throw new Exception("Wrong Logger Type");
                }
                _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);//verdiğimiz logger type'a göre bir loggerservice örneği oluşturur
            }
            
            base.RuntimeInitialize(method);
        }
        public override void OnException(MethodExecutionArgs args)//bir hata olduğu durumda hata gelsin ,aynı zamanda assembly'e ekledik
        {
            if (_loggerService!=null)
            {
                _loggerService.Error(args.Exception);
            }
        }

        //public override void OnEntry(MethodExecutionArgs args)
        //{
        //    if (!_loggerService.IsInfoEnabled)//eğer logger serviste info enabled durumda değilse loglama işlemini gerçekleştirme
        //    {
        //        return;
        //    }
        //    try//loglama yapamazsa sistemi durdurmasın diye
        //    {
        //        var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter//t : tip , i : iterator
        //        {
        //            Name = t.Name,
        //            Type = t.ParameterType.Name,
        //            Value = args.Arguments.GetArgument(i)

        //        }).ToList();
        //        var logDetail = new LogDetail
        //        {
        //            FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
        //            MethodName = args.Method.Name,
        //            LogParameters = logParameters
        //        };
        //        _loggerService.Info(logDetail);
        //    }
        //    catch (Exception)
        //    {
        //    }

        //}

    }
}
