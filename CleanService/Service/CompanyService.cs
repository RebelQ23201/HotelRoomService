using CleanService.DBContext;
using CleanService.IService;
using CleanService.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.Service
{
    public class CompanyService : ICompanyService
    {
        public async Task<IEnumerable<Company>> GetList()
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Company> list = await context.Companies.ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Company>();
        }
        public async Task<Company> GetById(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = await context.Companies.FindAsync(id);
                return company;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = await context.Companies.FindAsync(id);
                if (company == null)
                {
                    return false;
                }
                context.Companies.Remove(company);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(Company c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = await GetById(c.CompanyId);
                if (company == null)
                {
                    return false;
                }
                context.Entry(company).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }
    }
}
