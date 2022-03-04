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
    public class ServiceService : IBaseService<DBContext.Service>
    {
        public async Task<IEnumerable<DBContext.Service>> GetList(Expression<Func<DBContext.Service, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<DBContext.Service> list = await context.Services.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<DBContext.Service>();
        }
        public async Task<DBContext.Service> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                DBContext.Service service = await context.Services.FindAsync(id);
                return service;
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
                DBContext.Service service = await context.Services.FindAsync(id);
                if (service == null)
                {
                    return false;
                }
                context.Services.Remove(service);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(DBContext.Service c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                DBContext.Service service = await context.Services.FindAsync(c.ServiceId);
                if (service == null)
                {
                    return false;
                }
                context.Entry(service).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(DBContext.Service c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                DBContext.Service service = await context.Services.FindAsync(c.ServiceId);
                if (service != null)
                {
                    return false;
                }
                context.Services.AddAsync(c);
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
