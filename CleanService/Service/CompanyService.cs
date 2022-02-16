using CleanService.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.Service
{
    class CompanyService : IService<Company>
    {

        public IEnumerable<Company> GetList()
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = GetById(id);
                if (company == null)
                {
                    return false;
                }
                context.Companies.Remove(company);
                context.SaveChanges();
                companyList = InitList();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List;
        }
        public bool Delete(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = GetById(id);
                if (company == null)
                {
                    return false;
                }
                context.Companies.Remove(company);
                context.SaveChanges();
                companyList = InitList();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public Company GetById(int id)=> companyList.SingleOrDefault(c => c.CompanyId == id);

        public bool Update(Company c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Company company = GetById(c.CompanyId);
                if (company == null)
                {
                    return false;
                }
                context.Entry<Company>(company).State = EntityState.Modified;
                context.SaveChanges();
                companyList = InitList();
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
