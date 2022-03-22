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
    public class OrderService : IOrderService<Order>
    {
        public async Task<IEnumerable<Order>> GetList(Expression<Func<Order, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Order> list;
                if (isDeep.HasValue && isDeep.Value)
                {
                    list = await context.Orders.Where(query)
                        .Include(o => o.RoomOrders).ThenInclude(ro=>ro.Room)
                        .Include(o => o.RoomOrders).ThenInclude(ro=>ro.OrderDetails).ThenInclude(od=>od.Service)
                        .Include(o => o.RoomOrders).ThenInclude(ro=>ro.OrderDetails).ThenInclude(od=>od.Employee)
                        .Include(o=>o.Company)
                        .Include(o=>o.Hotel)
                        .ToArrayAsync();
                }
                else
                {
                    list = await context.Orders.Where(query).ToArrayAsync();
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Order>();
        }
        public async Task<int> Count(Expression<Func<Order, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();

                return context.Orders.Where(query).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }
        public async Task<Order> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Order order;
                if (isDeep.HasValue && isDeep.Value)
                {
                    order = await context.Orders
                        .Include(o => o.RoomOrders).ThenInclude(ro => ro.Room)
                        .Include(o => o.RoomOrders).ThenInclude(ro => ro.OrderDetails).ThenInclude(od => od.Service)
                        .Include(o => o.RoomOrders).ThenInclude(ro => ro.OrderDetails).ThenInclude(od => od.Employee)
                        .Include(o => o.Company)
                        .Include(o => o.Hotel)
                        .SingleOrDefaultAsync(o=>o.OrderId==id);
                }
                else
                {
                    order = await context.Orders.FindAsync(id);
                }
                return order;
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
                Order order = await context.Orders.FindAsync(id);
                if (order == null)
                {
                    return false;
                }
                context.Orders.Remove(order);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(Order c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Order order = await context.Orders.FindAsync(c.OrderId);
                if (order == null)
                {
                    return false;
                }
                order.Status = c.Status;
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Create(Order c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Order order = await context.Orders.FindAsync(c.OrderId);
                if (order != null)
                {
                    return false;
                }
                context.Orders.AddAsync(c);
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
            IEnumerable<Order> list = await context.Orders.ToListAsync();
            int total = list.Count();
            return total;
        }

        public async Task<double> GetTotalMoney(int orderId)
        {
            double totalMoney = 0;
            using CleanContext context = new CleanContext();
            List<RoomOrder> listRoomOrder = await context.RoomOrders.Where(ro => ro.OrderId == orderId).ToListAsync();
            foreach (RoomOrder roomOrder in listRoomOrder)
            {
                List<OrderDetail> list = await context.OrderDetails.Where(od => od.RoomOrderId == roomOrder.RoomOrderId).ToListAsync();
                foreach (OrderDetail orderDetail in list)
                {
                    totalMoney = totalMoney + (orderDetail.Price.Value * orderDetail.Quantity.Value);
                }
            }
            return totalMoney;
        }
    }
}
