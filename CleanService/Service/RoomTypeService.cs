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
    public class RoomTypeService : IRoomTypeService<RoomType>
    {
        public async Task<IEnumerable<RoomType>> GetList(Expression<Func<RoomType, bool>> query, bool? isDeep)
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
        public async Task<int> Count(Expression<Func<RoomType, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.RoomTypes.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<RoomType> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomType roomType = await context.RoomTypes.Where(rt => rt.RoomTypeId == id).Include(rt => rt.SystemRoomType).FirstAsync();
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

        public async Task<bool> DeleteByHotel(int roomTypeId, int hotelId)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomType roomType = await context.RoomTypes.FindAsync(roomTypeId);
                if (roomType == null || !roomType.HotelId.Equals(hotelId))
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

        public async Task<IEnumerable<RoomType>> GetRoomTypeByHotelId(int hotelId)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<RoomType> roomTypes = await context.RoomTypes.Where(s => s.HotelId == hotelId).Include(r => r.SystemRoomType).ToArrayAsync();
                return roomTypes;
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
            IEnumerable<RoomType> list = await context.RoomTypes.ToListAsync();
            int total = list.Count();
            return total;
        }

        public async Task<bool> UpdateByHotel(int hotelId, RoomType c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomType roomType = await context.RoomTypes.FindAsync(c.RoomTypeId);
                if (roomType == null || !roomType.HotelId.Equals(hotelId) || !roomType.HotelId.Equals(c.HotelId))
                {
                    return false;
                }
                roomType.SystemRoomTypeId = c.SystemRoomTypeId;
                roomType.Name = c.Name;
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
    }
}
