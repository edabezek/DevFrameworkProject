using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DatabaseLogger : LoggerService //database'e loglayacağız
    {
        public DatabaseLogger() : base(LogManager.GetLogger("DatabaseLogger"))//Log bilgisini configuration dosyasında yazacağımız DatabaseLogger isimli loggerdan al 
        {
        }
    }
}
