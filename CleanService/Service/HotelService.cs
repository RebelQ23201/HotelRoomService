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
    public class HotelService : IHotelService<Hotel>
    {
        public async Task<IEnumerable<Hotel>> GetList(Expression<Func<Hotel, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Hotel> list;
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await context.Hotels.Where(query)
                        .Include(h => h.RoomTypes)
                        .ThenInclude(rt=>rt.SystemRoomType)
                        .ToArrayAsync();
                }
                else
                {
                    list = await context.Hotels.Where(query)
                        .Include(h => h.RoomTypes)
                        .ThenInclude(rt => rt.SystemRoomType)
                        .ToArrayAsync();
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Hotel>();
        }
        public async Task<int> Count(Expression<Func<Hotel, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.Hotels.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<Hotel> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Hotel hotel;
                if (isDeep.HasValue && isDeep.Value)
                {
                    hotel = await context.Hotels
                        .Include(x => x.RoomTypes)
                        .ThenInclude(rt=>rt.SystemRoomType)
                        .SingleOrDefaultAsync(x => x.HotelId == id);
                }
                else
                {
                    hotel = await context.Hotels.FindAsync(id);
                }
                return hotel;
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
                Hotel hotel = await context.Hotels.FindAsync(id);
                if (hotel == null)
                {
                    return false;
                }
                context.Hotels.Remove(hotel);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(Hotel c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Hotel hotel = await context.Hotels.FindAsync(c.HotelId);
                if (hotel == null)
                {
                    return false;
                }
                hotel.Name = c.Name;
                hotel.Address = c.Address;
                hotel.Phone = c.Phone;
                hotel.Email = c.Email;
                context.Entry(hotel).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Create(Hotel c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Hotel hotel = await context.Hotels.FindAsync(c.HotelId);
                if (hotel != null)
                {
                    return false;
                }
                context.Hotels.AddAsync(c);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<Hotel> GetEmail(string email)
        {
            try
            {
                using CleanContext context = new CleanContext();

                Hotel hotel = await context.Hotels.Where(acc => acc.Email.Equals(email)).FirstOrDefaultAsync();

                if (hotel == null)
                {
                    return null;
                }


                return hotel;
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
            IEnumerable<Hotel> list = await context.Hotels.ToListAsync();
            int total = list.Count();
            return total;
        }
    }
}
