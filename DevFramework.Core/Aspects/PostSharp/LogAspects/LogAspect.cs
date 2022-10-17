using DevFramework.Core.CrossCuttingConcerns.Logging;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method,TargetMemberAttributes =MulticastAttributes.Instance)]//nesne instancelerinin metotlarını uygula,constructor'ını uygulama diyoruz.
    public class LogAspect : OnMethodBoundaryAspect
    {
        //neyle loglayacağız
        private Type _loggerType;
        LoggerService _loggerService;//gelen nesneyi atıyoruz.
        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType!=typeof(LoggerService))//logger type yanlışsa ex
            {
                throw new Exception("Wrong Logger Type");
            }
            _loggerService=(LoggerService)Activator.CreateInstance(_loggerType);
            base.RuntimeInitialize(method);
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!_loggerService.IsInfoEnabled)//eğer logger serviste info enabled durumda değilse loglama işlemini gerçekleştirme
            {
                return;
            }
            try//loglama yapamazsa sistemi durdurmasın diye
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter//t : tip , i : iterator
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)

                }).ToList();
                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    LogParameters = logParameters
                };
                _loggerService.Info(logDetail);
            }
            catch (Exception)
            {
            }
            
        }

    }
}
