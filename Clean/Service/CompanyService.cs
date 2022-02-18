using AutoMapper;
using Clean.DataContext;
using Clean.Model;
using Clean.Repository;
using Microsoft.AspNetCore.Mvc;
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
        private readonly CompanyRepository companyRepo;
        public CompanyService()
        {
            companyRepo = new CompanyRepository();
        }

        public async Task<IEnumerable<Company>> getCompanies()
        {
            var companies = await companyRepo.GetCompanies();
            return companies;
        }
        public async Task<Company> GetCompany(int id)
        {
            var company = await companyRepo.GetCompanyAsync(id);
            return company;
        }

        public async Task<string> editCompany(int id, Company company)
        {
            var message = await companyRepo.editCompany(id, company);
            return message;
        }

        public async Task<Company> addCompany (Company company)
        {
            var companyAdded = await companyRepo.addCompany(company);
            return companyAdded;
        }

        public async Task<string> deleteCompany(int id)
        {
            var message = await companyRepo.deleteCompany(id);
            return message;
        }
    }
}
