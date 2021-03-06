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
    public class CompanyService : ICompanyService<Company>
    {
        public async Task<IEnumerable<Company>> GetList(Expression<Func<Company, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Company> list;
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await context.Companies.Where(query)
                        .Include(c => c.Employees)
                        .Include(c=>c.Services)
                        .ToArrayAsync();
                }
                else
                {
                    list = await context.Companies.Where(query)
                        .Include(c => c.Employees)
                        .Include(c => c.Services)
                        .ToArrayAsync();
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Company>();
        }

        public async Task<int> Count(Expression<Func<Company, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.Companies.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<Company> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company;
                if (isDeep.HasValue && isDeep.Value)
                {
                    company = await context.Companies
                        .Include(c => c.Employees)
                        .Include(c => c.Services)
                        .SingleOrDefaultAsync(x=>x.CompanyId==id);
                }
                else
                {
                    company = await context.Companies.FindAsync(id);
                }
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
                company.Name = c.Name;
                company.Address = c.Address;
                company.Phone = c.Phone;
                company.Email = c.Email;
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
        public async Task<Company> GetEmail(string email)
        {
            try
            {
                using CleanContext context = new CleanContext();

                Company company = await context.Companies.Where(acc => acc.Email.Equals(email)).FirstOrDefaultAsync();

                if (company == null)
                {
                    return null;
                }


                return company;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }

        public async Task<int> GetTotal()
        {
            using CleanContext context = new CleanContext();
            IEnumerable<Company> list = await context.Companies.ToListAsync();
            int total = list.Count();
            return total;
        }
    }
}
