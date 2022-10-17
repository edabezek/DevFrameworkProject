using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string FullName { get; set; }//namespace'de barındıran sınıfa karşılık gelecek
        public string MethodName { get; set; }//o sınıftaki hangi metot
        public List<LogParameter> LogParameters { get; set; }//metodun parametreleri,birden fazla olduğu için List yazdık 
    }
}
