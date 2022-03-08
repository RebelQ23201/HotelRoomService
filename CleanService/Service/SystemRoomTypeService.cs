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
    public class SystemRoomTypeService : ISystemRoomTypeService<SystemRoomType>
    {
        public async Task<IEnumerable<SystemRoomType>> GetList(Expression<Func<SystemRoomType, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<SystemRoomType> list = await context.SystemRoomTypes.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<SystemRoomType>();
        }
        public async Task<SystemRoomType> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                SystemRoomType systemRoomType = await context.SystemRoomTypes.FindAsync(id);
                return systemRoomType;
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
                SystemRoomType systemRoomType = await context.SystemRoomTypes.FindAsync(id);
                if (systemRoomType == null)
                {
                    return false;
                }
                context.SystemRoomTypes.Remove(systemRoomType);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(SystemRoomType c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                SystemRoomType systemRoomType = await context.SystemRoomTypes.FindAsync(c.SystemRoomTypeId);
                if (systemRoomType == null)
                {
                    return false;
                }
                systemRoomType.Name = c.Name;
                context.Entry(systemRoomType).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(SystemRoomType c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                SystemRoomType systemRoomType = await context.SystemRoomTypes.FindAsync(c.SystemRoomTypeId);
                if (systemRoomType != null)
                {
                    return false;
                }
                context.SystemRoomTypes.AddAsync(c);
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
            IEnumerable<SystemRoomType> list = await context.SystemRoomTypes.ToListAsync();
            int total = list.Count();
            return total;
        }
    }
}
