using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; }//product
        public string Type { get; set; }//Product
        public object Value { get; set; }//güncelenen değer
    }
}
