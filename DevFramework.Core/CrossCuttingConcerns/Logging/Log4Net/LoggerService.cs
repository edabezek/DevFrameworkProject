using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class LoggerService
    {
        private ILog _log;

        public LoggerService(ILog log)
        {
            _log = log;
        }
        public bool IsInfoEnabled{ get { return _log.IsInfoEnabled; } }//info mesaj logları açık mı
        public bool IsDebugEnabled { get { return _log.IsDebugEnabled; } }  //debug logları açık mı,kim ne çağırdı
        public bool IsWarnEnabled { get { return _log.IsWarnEnabled; } }    //uyarı logları
        public bool IsFatalEnabled { get { return _log.IsFatalEnabled; } }  
        public bool IsErrorEnabled { get { return _log.IsErrorEnabled; } }  

        public void Info(object logMessage)//bize log mesajını ver loglayalım 
        {
            if (IsInfoEnabled)//info varsa ,info olarak logla
            {
                _log.Info(logMessage);  
            }
        }
        public void Debug(object logMessage)
        {
            if (IsInfoEnabled)
            {
                _log.Info(logMessage);
            }
        }
        public void Warn(object logMessage)
        {
            if (IsInfoEnabled)
            {
                _log.Info(logMessage);
            }
        }
        public void Fatal(object logMessage)
        {
            if (IsInfoEnabled)
            {
                _log.Info(logMessage);
            }
        }
        public void Error(object logMessage)
        {
            if (IsInfoEnabled)
            {
                _log.Info(logMessage);
            }
        }
    }
}
