using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class SerializableLogEvent 
    {
        private LoggingEvent _loggingEvent;//log4net'den gelir,log datasını içinde barındırır.

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }
        //log işlemine sebep olan kişi kim , read only
        public string UserName 
        { 
            get { return _loggingEvent.UserName; }  
        }
        //log datasının mesajını tutacağız
        public object MessageObject
        {
            get { return _loggingEvent.MessageObject; }
        }
       

    }
}
