using AutoMapper;
using Clean.DataContext;
using Clean.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Service
{
    public class CompanyService
    {
        private readonly CleanDBContext _context;
        private readonly IMapper mappper;

        public CompanyService()
        {
        }

        public CompanyService(CleanDBContext context, IMapper mappper)
        {
            _context = context;
            this.mappper = mappper;
        }

        public CompanyModel GetCompany(int id)
        {
            var company = _context.Companies.FindAsync(id);

            if (company == null)
            {
                return null;
            }

            CompanyModel model = mappper.Map<CompanyModel>(company);

            return model;
        }
    }
}
