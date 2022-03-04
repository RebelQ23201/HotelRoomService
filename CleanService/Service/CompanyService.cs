using CleanService.DBContext;
using CleanService.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.Service
{
    public class CompanyService : IBaseService<Company>
    {
        public async Task<IEnumerable<Company>> GetList(Expression<Func<Company, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Company> list = await context.Companies.Where(query).ToArrayAsync();
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
            //return false;
        }



        public async Task<bool> Update(Company c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = await context.Companies.FindAsync(c.CompanyId);
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
            //return false;
        }



        public async Task<bool> Create(Company c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = await context.Companies.FindAsync(c.CompanyId);
                if (company != null)
                {
                    return false;
                }
                await context.Companies.AddAsync(c);
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
