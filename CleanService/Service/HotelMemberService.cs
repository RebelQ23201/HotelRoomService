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
    public class HotelMemberService : IBaseService<HotelMember>
    {
        public async Task<IEnumerable<HotelMember>> GetList(Expression<Func<HotelMember, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<HotelMember> list = await context.HotelMembers.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<HotelMember>();
        }
        public async Task<HotelMember> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                HotelMember hotelMember = await context.HotelMembers.FindAsync(id);
                return hotelMember;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }
        public async Task<int> Count(Expression<Func<HotelMember, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.HotelMembers.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                HotelMember hotelMember = await context.HotelMembers.FindAsync(id);
                if (hotelMember == null)
                {
                    return false;
                }
                context.HotelMembers.Remove(hotelMember);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(HotelMember c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                HotelMember hotelMember = await context.HotelMembers.FindAsync(c.MemberId);
                if (hotelMember == null)
                {
                    return false;
                }
                context.Entry(hotelMember).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(HotelMember c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                HotelMember hotelMember = await context.HotelMembers.FindAsync(c.MemberId);
                if (hotelMember != null)
                {
                    return false;
                }
                context.HotelMembers.AddAsync(c);
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

