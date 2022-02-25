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
    class RoomServiceService : IBaseService<RoomService>
    {
        public async Task<IEnumerable<RoomService>> GetList(Expression<Func<RoomService, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<RoomService> list = await context.RoomServices.Where(query).ToArrayAsync();

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<RoomService>();
        }
        public async Task<RoomService> GetById(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomService roomService = await context.RoomServices.FindAsync(id);
                return roomService;
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
                RoomService roomService = await context.RoomServices.FindAsync(id);
                if (roomService == null)
                {
                    return false;
                }
                context.RoomServices.Remove(roomService);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(RoomService c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomService roomService = await context.RoomServices.FindAsync(c.ServiceId);
                if (roomService == null)
                {
                    return false;
                }
                context.Entry(roomService).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Create(RoomService c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomService roomService = await context.RoomServices.FindAsync(c.ServiceId);
                if (roomService != null)
                {
                    return false;
                }
                context.RoomServices.AddAsync(c);
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
