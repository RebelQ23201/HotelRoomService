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
    class EmployeeService:IBaseService<Employee>
    {
        public async Task<IEnumerable<Employee>> GetList(Expression<Func<Employee, bool>> query)
        {
            try
            {
                using CleanContext context = new CleanContext();
                IEnumerable<Employee> list = await context.Employees.Where(query).ToArrayAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Employee>();
        }
        public async Task<Employee> GetById(int id)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Employee employee = await context.Employees.FindAsync(id);
                return employee;
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
                Employee employee = await context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return false;
                }
                context.Employees.Remove(employee);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }



        public async Task<bool> Update(Employee c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Employee employee = await context.Employees.FindAsync(c.EmployeeId);
                if (employee == null)
                {
                    return false;
                }
                context.Entry(employee).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<bool> Create(Employee c)
        {
            try
            {
                using CleanContext context = new CleanContext();
                Employee employee = await context.Employees.FindAsync(c.EmployeeId);
                if (employee != null)
                {
                    return false;
                }
                context.Employees.AddAsync(c);
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
