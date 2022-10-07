using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.Core.Aspects.PostSharp.TransactionAspect
{
    [Serializable]
    public class TransactionScopeAspect : OnMethodBoundaryAspect
    {
        private TransactionScopeOption _option;
        public TransactionScopeAspect(TransactionScopeOption option)//parametreli de yapılabilir.
        {
           _option= option; 
        }
        public TransactionScopeAspect()
        {
                
        }
        public override void OnEntry(MethodExecutionArgs args)//metodun başında-girildiğinde açılacak
        {
            args.MethodExecutionTag=new TransactionScope(_option);//metodu execute edeceğimiz tag bir TransactionScope olacak,varsa option gelcek yoksa parametresiz çalışacak
        }
        public override void OnSuccess(MethodExecutionArgs args)//try'ın içi
        {
            ((TransactionScope)args.MethodExecutionTag).Complete();//metot başarılıysa try'dan geçtiyse complete'i çağır
        }
        public override void OnExit(MethodExecutionArgs args)//metot başarılı değilse 
        {
            ((TransactionScope)args.MethodExecutionTag).Dispose();
        }
    }
}
