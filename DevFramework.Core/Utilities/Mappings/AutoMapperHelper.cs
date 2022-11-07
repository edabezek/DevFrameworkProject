using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Utilities.Mappings
{
    public class AutoMapperHelper
    {
        //aynı tipteki bir listenin mapping işlemi için , generic mapper
        //list -> list
        public static List<T> MapToSameTypeList<T>(List<T> list)
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<T, T>();
            });

            List<T> results = Mapper.Map<List<T>, List<T>>(list);
            return results;
        }
        //product -> product
        public static T MapToSameType<T>(T obj)
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<T, T>();
            });

            T results = Mapper.Map<T, T>(obj);
            return results;
        }
    }
}
