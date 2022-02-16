using AutoMapper;
using Clean.DataContext;
using Clean.Model;
using Clean.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Repository
{
    public class CompanyRepository
    {
        private readonly CleanDBContext _context;
        private readonly IMapper mappper;

        public CompanyRepository()
        {
            _context = new CleanDBContext();
        }

        public CompanyRepository(CleanDBContext context, IMapper mappper)
        {
            _context = context;
            this.mappper = mappper;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            List<Company> companies = await _context.Companies.ToListAsync();
            return companies;
        }
        public async Task<Company> GetCompanyAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            return company;
        }
    }
}
