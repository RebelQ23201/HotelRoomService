﻿using CleanService.DBContext;
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
    class HotelService : IBaseService<Hotel>
    {
        public async Task<IEnumerable<Hotel>> GetList(Expression<Func<Hotel, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Hotel> list = await context.Hotels.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Hotel>();
        }
        public async Task<Hotel> GetById(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Hotel hotel = await context.Hotels.FindAsync(id);
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
    }
}