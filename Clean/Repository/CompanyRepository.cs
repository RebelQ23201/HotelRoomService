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
        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
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

        public async Task<string> editCompany(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return "The id of the company does not match with the requested id";
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return "Can not find company with the ID: " + id;
                }
                else
                {
                    throw;
                }
            }

            return "Update company successfully";
        }
        public async Task<Company> addCompany (Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<string> deleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return "Can not find any company with ID: " + id;
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return "Delete Success";
        }
    }
}
