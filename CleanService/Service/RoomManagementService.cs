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
    public class RoomManagementService : IBaseService<Room>
    {
        public async Task<IEnumerable<Room>> GetList(Expression<Func<Room, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Room> list = await context.Rooms.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Room>();
        }
        public async Task<int> Count(Expression<Func<Room, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.Rooms.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<Room> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Room room = await context.Rooms.FindAsync(id);
                return room;
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
                Room room = await context.Rooms.FindAsync(id);
                if (room == null)
                {
                    return false;
                }
                context.Rooms.Remove(room);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(Room c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Room room = await context.Rooms.FindAsync(c.RoomId);
                if (room == null)
                {
                    return false;
                }
                context.Entry(room).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Create(Room c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Room room = await context.Rooms.FindAsync(c.RoomId);
                if (room != null)
                {
                    return false;
                }
                context.Rooms.AddAsync(c);
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