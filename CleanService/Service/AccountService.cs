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
    public class AccountService : IAccountService<Account>
    {
        public async Task<IEnumerable<Account>> GetList(Expression<Func<Account, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Account> list;
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await context.Accounts.Where(query)
                        .Include(x => x.Role)
                        .ToArrayAsync();
                }
                else
                {
                    list = await context.Accounts.Where(query).ToArrayAsync();
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Account>();
        }

        public async Task<int> Count(Expression<Func<Account, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.Accounts.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<Account> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Account account;
                if (isDeep.HasValue && isDeep.Value)
                {
                    account = await context.Accounts
                        .Include(x => x.Role)
                        .SingleOrDefaultAsync(x=>x.AccountId==id);
                }
                else
                {
                    account = await context.Accounts.FindAsync(id);
                }
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

        public async Task<Account> GetEmail(string email)
        {
            
            try
            {
                using CleanContext context = new CleanContext();

                Account account = await context.Accounts.Where(acc => acc.Email.Equals(email)).Include(a=>a.Role).FirstOrDefaultAsync();

                if (account == null)
                {
                    return null;
                }
                
                
                return account;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }

        public async Task<bool> CreateViaSignIn(string email)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Account> list = await context.Accounts.ToArrayAsync();

                Account c = new Account();
                c.RoleId = 1;
                c.Email = email;

                System.Diagnostics.Debug.WriteLine(c.AccountId + " " + c.RoleId + " " + c.Email);

                await context.Accounts.AddAsync(c);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<int> GetTotal()
        {
            using CleanContext context = new CleanContext();
            IEnumerable<Account> list = await context.Accounts.ToListAsync();
            int total = list.Count();
            return total;
        }
    }
}
