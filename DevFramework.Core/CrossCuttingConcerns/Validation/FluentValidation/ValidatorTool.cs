using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.Entities;

namespace DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public class ValidatorTool 
    {
        public static void FluentValidate(IValidator validator,object entity)
        {
            //Bu şekilde testten geçmedi
            //var result = validator.Validate(entity); //The error message is quite clear - your Product class does not implement the IValidationContext interface. It looks like you're passing in the wrong value to your ValidatorTool.FluentValidate method.


            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            if (result.Errors.Count>0)//bir hata varsa
            {
                throw new ValidationException(result.Errors);  
            }
        }
    }
}
