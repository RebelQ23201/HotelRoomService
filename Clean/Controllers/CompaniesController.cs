using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clean.Model.Output;
using AutoMapper;
using CleanService.IService;
using CleanService.Service;
using CleanService.DBContext;
using System.Linq.Expressions;
using Clean.Util;
using Clean.Model.Input;

namespace Clean.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService<Company> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Company> companyService, IMapper mappper)
        public CompaniesController(ICompanyService<Company> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyOutputModel>>> GetCompanies(
            [FromQuery] int? id, string name, string addr, string phone, string email, bool? detailed =false)
        {
            Expression<Func<Company, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.CompanyId == id);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                filters = filters.AndAlso(c => c.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(addr))
            {
                filters = filters.AndAlso(c => c.Address == addr);
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                filters = filters.AndAlso(c => c.Phone == phone);
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                filters.AndAlso(c => c.Email == email);
            }
            List<Company> companies = (await service.GetList(filters, false)).ToList();
            List<CompanyOutputModel> models = mappper.Map<List<CompanyOutputModel>>(companies);
            return models;
        }
        [HttpGet("Ids")]
        public async Task<ActionResult<IEnumerable<int>>> GetId(
            [FromQuery] int? id, string name, string addr, string phone, string email, bool? detailed = false)
        {
            Expression<Func<Company, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.CompanyId == id);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                filters = filters.AndAlso(c => c.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(addr))
            {
                filters = filters.AndAlso(c => c.Address == addr);
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                filters = filters.AndAlso(c => c.Phone == phone);
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                filters.AndAlso(c => c.Email == email);
            }
            var companies = (await service.GetList(filters, false));
            List<int> models = companies.Select(c => c.CompanyId).ToList();
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyOutputModel>> GetCompany(int id, bool? detailed =true)
        {
            Company company = await service.GetById(id, detailed);

            if (company == null)
            {
                return NotFound();
            }

            CompanyOutputModel model = mappper.Map<CompanyOutputModel>(company);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyInputModel companyInput)
        {
            if (id != companyInput.CompanyId)
            {
                return BadRequest();
            }

            Company company = new Company();
            company.CompanyId = companyInput.CompanyId;
            company.Name = companyInput.Name;
            company.Address = companyInput.Address;
            company.Phone = companyInput.Phone;
            company.Email = companyInput.Email;

            if (!await service.Update(company))
            {
                return NotFound();
            }

            return Content("Update success");
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyOutputModel>> PostCompany(Company company)
        {
            //_context.Companies.Add(company);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            if (!await service.Create(company))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetCompanies), new { id = company.CompanyId }, company);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            //var company = await _context.Companies.FindAsync(id);
            //if (company == null)
            //{
            //    return NotFound();
            //}

            //_context.Companies.Remove(company);
            //await _context.SaveChangesAsync();
            if (!await service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool CompanyExists(int id)
        //{
        //    return _context.Companies.Any(e => e.CompanyId == id);
        //}
    }
}

