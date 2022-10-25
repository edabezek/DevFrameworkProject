using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.PerformanceAspects
{
    [Serializable]
    public class PerformanceCounterAspect : OnMethodBoundaryAspect//metodun başında kronometre açıp sonunda durduracağız.
    {
        private int _interval;//arada geçen süre
        [NonSerialized]//Stopwatch serialize edilebilen bir nesne değil 
        private Stopwatch _stopwatch; //kronometre

        public PerformanceCounterAspect(int interval=5)//metodun çalışma süresi 5sn den fazla ise işlem yapacağız
        {
            _interval = interval;
        }
        public override void RuntimeInitialize(MethodBase method)//bir örnek oluşturacağımız zaman reflection ile yapıyoruz postsharp'da
        {
            _stopwatch = Activator.CreateInstance<Stopwatch>();//Stopwatch için bir instance oluştur diyoruz.
        }
        public override void OnEntry(MethodExecutionArgs args)//metodun girişinde kronometreyi açacağız
        {
            _stopwatch.Start();
            base.OnEntry(args);
        }
        public override void OnExit(MethodExecutionArgs args)//metodun çıkışında kapatacağız
        {
            _stopwatch.Stop();
            if (_stopwatch.Elapsed.TotalSeconds>_interval)//eğer stopwatch'ın geçen süresi saniye olarak bizim tanımladığımız interval'den büyük ise
            {
                //burada loglayabiliriz yada mail gönderebiliriz.Biz output'a yazıyoruz
                //args.Method.DeclaringType.FullName tipin full name'ini,hangi sınıfta böyle bir işlem olduğu
                //args.Method.Name o sınıftaki hangi metotta 
                //_stopwatch.Elapsed.TotalSeconds geçen süre
                Debug.WriteLine("Performance: {0}.{1}-->>{2}",args.Method.DeclaringType.FullName, args.Method.Name,_stopwatch.Elapsed.TotalSeconds);
            }
            _stopwatch.Reset();//işlemden sonra kronometre sıfırlanır
            base.OnExit(args);
        }
    }
}
