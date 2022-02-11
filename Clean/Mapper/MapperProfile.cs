using Clean.DataContext;
using Clean.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Mapper
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<CompanyModel, Company>()
                .ReverseMap();
        }
    }
}
