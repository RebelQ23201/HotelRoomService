using AutoMapper;
using Clean.Filter;
using Clean.Model.Input;
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
    [TokenAuthenticationFilter]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService<Employee> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Employee> accountService, IMapper mappper)
        public EmployeesController(IEmployeeService<Employee> service, IMapper mappper)
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
            Employee employee = await service.GetById(id, detailed);

            if (employee == null)
            {
                return NotFound();
            }

            EmployeeOutputModel model = mappper.Map<EmployeeOutputModel>(employee);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            if (!await service.Update(employee))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeOutputModel>> PostEmployee([FromBody] EmployeePostInputModel employeeInput)
        {
            int id = await service.GetTotal();
            Employee model = new Employee();
            model.EmployeeId = id + 1;
            model.CompanyId = employeeInput.CompanyId;
            model.Name = employeeInput.Name;
            model.Address = employeeInput.Address;
            model.Phone = employeeInput.Phone;
            model.Status = 1;
            if (!await service.Create(model))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetEmployee), new { id = id }, model);
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

        [Route("Comapny")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<EmployeeOutputModel>>> GetEmployeeByCompanyId(int id)
        {
            IEnumerable<Employee> employees = await service.GetEmployeeByCompanyId(id);

            if (employees == null)
            {
                return NotFound();
            }

            List<EmployeeOutputModel> models = mappper.Map<List<EmployeeOutputModel>>(employees);
            //ServiceOutputModel model = mappper.Map<ServiceOutputModel>(account);

            return models;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        [Route("Company")]
        public async Task<IActionResult> EditEmployee(int id, int companyId, EmployeeInputModel employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            Employee model = new Employee();
            model.EmployeeId = id;
            model.CompanyId = employee.CompanyId;
            model.Name = employee.Name;
            model.Address = employee.Address;
            model.Phone = employee.Phone;
            model.Status = 1;

            if (!await service.UpdateByCompany(companyId, model))
            {
                return NotFound();
            }

            return Content("Update Employee id " + id + " Successfully");
        }

        [HttpDelete()]
        [Route("Company")]
        public async Task<IActionResult> DeleteByCompany(int id, int companyId)
        {
            if (!await service.DeleteByCompany(id, companyId))
            {
                return NotFound();
            }

            return Content("Delete Employee id " + id + " Successfully");
        }
    }
}
