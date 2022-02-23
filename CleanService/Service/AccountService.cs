using CleanService.DBContext;
using CleanService.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.Service
{
    public class AccountService : IBaseService<Account>
    {
        public async Task<IEnumerable<Account>> GetList(Expression<Func<Account, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Account> list = await context.Accounts.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Account>();
        }
        public async Task<Account> GetById(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Account account = await context.Accounts.FindAsync(id);
                return account;
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
                Account account = await context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return false;
                }
                context.Accounts.Remove(account);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(Account c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Account account = await context.Accounts.FindAsync(c.AccountId);
                if (account == null)
                {
                    return false;
                }
                context.Entry(account).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(Account c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Account account = await context.Accounts.FindAsync(c.AccountId);
                if (account != null)
                {
                    return false;
                }
                context.Accounts.AddAsync(c);
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
