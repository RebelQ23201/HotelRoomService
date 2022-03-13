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
    public class RoleService : IBaseService<Role>
    {
        public async Task<IEnumerable<Role>> GetList(Expression<Func<Role, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Role> list = await context.Roles.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Role>();
        }
        public async Task<int> Count(Expression<Func<Role, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.Roles.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<Role> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Role role = await context.Roles.FindAsync(id);
                return role;
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
                Role role = await context.Roles.FindAsync(id);
                if (role == null)
                {
                    return false;
                }
                context.Roles.Remove(role);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(Role c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Role role = await context.Roles.FindAsync(c.RoleId);
                if (role == null)
                {
                    return false;
                }
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(Role c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Role role = await context.Roles.FindAsync(c.RoleId);
                if (role != null)
                {
                    return false;
                }
                context.Roles.AddAsync(c);
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

