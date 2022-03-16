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
    public class RoomOrderService : IRoomOrderService<RoomOrder>
    {
        public async Task<IEnumerable<RoomOrder>> GetList(Expression<Func<RoomOrder, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<RoomOrder> list = await context.RoomOrders.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<RoomOrder>();
        }
        public async Task<int> Count(Expression<Func<RoomOrder, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.RoomOrders.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<RoomOrder> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomOrder roomOrder = await context.RoomOrders.FindAsync(id);
                return roomOrder;
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
                RoomOrder roomOrder = await context.RoomOrders.FindAsync(id);
                if (roomOrder == null)
                {
                    return false;
                }
                context.RoomOrders.Remove(roomOrder);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(RoomOrder c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomOrder roomOrder = await context.RoomOrders.FindAsync(c.RoomOrderId);
                if (roomOrder == null)
                {
                    return false;
                }
                context.Entry(roomOrder).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(RoomOrder c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                RoomOrder roomOrder = await context.RoomOrders.FindAsync(c.RoomOrderId);
                if (roomOrder != null)
                {
                    return false;
                }
                context.RoomOrders.AddAsync(c);
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
            IEnumerable<RoomOrder> list = await context.RoomOrders.ToListAsync();
            int total = list.Count();
            return total;
        }
    }
}
