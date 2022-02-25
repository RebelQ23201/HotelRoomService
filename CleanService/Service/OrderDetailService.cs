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
    class OrderDetailService : IBaseService<OrderDetail>
    {
        public async Task<IEnumerable<OrderDetail>> GetList(Expression<Func<OrderDetail, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<OrderDetail> list = await context.OrderDetails.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<OrderDetail>();
        }
        public async Task<OrderDetail> GetById(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                OrderDetail orderDetail = await context.OrderDetails.FindAsync(id);
                return orderDetail;
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
                OrderDetail orderDetail = await context.OrderDetails.FindAsync(id);
                if (orderDetail == null)
                {
                    return false;
                }
                context.OrderDetails.Remove(orderDetail);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(OrderDetail c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                OrderDetail orderDetail = await context.OrderDetails.FindAsync(c.OrderDetailId);
                if (orderDetail == null)
                {
                    return false;
                }
                context.Entry(orderDetail).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Create(OrderDetail c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                OrderDetail orderDetail = await context.OrderDetails.FindAsync(c.OrderDetailId);
                if (orderDetail != null)
                {
                    return false;
                }
                context.OrderDetails.AddAsync(c);
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
