using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.ValidationRules.FluentValidation
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.CategoryId).NotEmpty();
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.QuantityPerUnit).NotEmpty();

            RuleFor(p => p.ProductName).Length(2,20);//min 2 karakter,max 20
            RuleFor(p => p.UnitPrice).GreaterThan(20).When(p => p.CategoryId == 1);//categoryId'si 1 olduğunda ürün fiyatının 20'den büyük olmalı
            //kendi kuralımızı yazarsak 
            //RuleFor(p => p.ProductName).Must(StartWithA);
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");//A ile başlıyorsa true olur ve rulefor çalışır.
        }
    }
}
