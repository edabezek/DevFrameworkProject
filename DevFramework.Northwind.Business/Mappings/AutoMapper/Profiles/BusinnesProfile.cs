﻿using AutoMapper;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.Mappings.AutoMapper.Profiles
{
    public class BusinnesProfile : Profile
    {
        public BusinnesProfile()
        {
            CreateMap<Product, Product>();

        }
    }
}
