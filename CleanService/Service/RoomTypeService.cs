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
    class RoomTypeService : IBaseService<RoomType>
    {
        public async Task<IEnumerable<RoomType>> GetList(Expression<Func<RoomType, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<RoomType> list = await context.RoomTypes.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<RoomType>();
        }
        public async Task<RoomType> GetById(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomType roomType = await context.RoomTypes.FindAsync(id);
                return roomType;
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
                RoomType roomType = await context.RoomTypes.FindAsync(id);
                if (roomType == null)
                {
                    return false;
                }
                context.RoomTypes.Remove(roomType);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(RoomType c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomType roomType = await context.RoomTypes.FindAsync(c.RoomTypeId);
                if (roomType == null)
                {
                    return false;
                }
                context.Entry(roomType).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(RoomType c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomType roomType = await context.RoomTypes.FindAsync(c.RoomTypeId);
                if (roomType != null)
                {
                    return false;
                }
                context.RoomTypes.AddAsync(c);
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
