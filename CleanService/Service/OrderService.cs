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
    public class OrderService : IBaseService<Order>
    {
        public async Task<IEnumerable<Order>> GetList(Expression<Func<Order, bool>> query, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Order> list = await context.Orders.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Order>();
        }
        public async Task<Order> GetById(int id, bool? isDeep)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Order order = await context.Orders.FindAsync(id);
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
    }
}
