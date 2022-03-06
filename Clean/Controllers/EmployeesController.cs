﻿using AutoMapper;
using Clean.Model.Output;
using Clean.Util;
using CleanService.DBContext;
using CleanService.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clean.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IBaseService<Employee> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Employee> accountService, IMapper mappper)
        public EmployeesController(IBaseService<Employee> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeOutputModel>>> GetEmployees(
            [FromQuery] int? id, bool? detailed =false)
        {
            Expression<Func<Employee, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.EmployeeId == id);
            }

            List<Employee> accounts = (await service.GetList(filters, detailed)).ToList();
            List<EmployeeOutputModel> models = mappper.Map<List<EmployeeOutputModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeOutputModel>> GetEmployee(int id, bool? detailed =true)
        {
            Employee account = await service.GetById(id, detailed);

            if (account == null)
            {
                return NotFound();
            }

            EmployeeOutputModel model = mappper.Map<EmployeeOutputModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee account)
        {
            if (id != account.EmployeeId)
            {
                return BadRequest();
            }

            if (!await service.Update(account))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeOutputModel>> PostEmployee(Employee account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetEmployees), new { id = account.EmployeeId }, account);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            if (!await service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
