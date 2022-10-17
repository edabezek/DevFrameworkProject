using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Layouts //loglanacak datanın loglanma formatı 
{
    public class JsonLayout : LayoutSkeleton
    {
        public override void ActivateOptions()
        {
            
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)//logevent'ini json'a çevireceğiz 
        {
            var logEvent=new SerializableLogEvent(loggingEvent);//loglacak datayı getirdik
            var json = JsonConvert.SerializeObject(logEvent,Formatting.Indented); //klasik json formatı ile yazar
            writer.WriteLine(json); 
        }
    }
}
