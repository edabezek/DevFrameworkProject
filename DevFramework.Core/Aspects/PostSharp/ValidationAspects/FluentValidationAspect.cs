using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.ValidationAspects
{
    [Serializable]//compile time için
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        //[FluentValidationAspect(typeof(ProductValidator))] için ProductValidator'a ihtiyacımız var,constructor ile vereceğiz
        Type _validatorType;
        public FluentValidationAspect(Type validatorType)
        {
            _validatorType = validatorType;
        }
        public override void OnEntry(MethodExecutionArgs args) //metoda girdiğinde doğrulama yapacak
        {
            //burada ValidatorTool'u çağırmamız lazım,instance'ını oluştururuz.bize validator çağıracak
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //birde entity gelecek,onu validate edeceğiz,fakat tipi belli değil-Product yada Category gelebilir- o yüzden 
            //ProductValidator'ın BaseType'ının(yani AbstractValidator'ının),birinci argümanın generic tipini al diyeceğiz.Yani product
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //product manager'da add metoduna geldi,çalışacağımız add metodunun içindeki product'u arayacağız,
            //validate edilecek entities
            //product manager'da çalıştığım metodun parametrelerini gezip,product olanları yakalıyoruz.
            var entities = args.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                //validator tool'u kullanarak gönderdiğim productvalidator'ı kullanarak entity'i validate etmiş olacağız
                ValidatorTool.FluentValidate(validator, entity);
            }
        }
    }
}
