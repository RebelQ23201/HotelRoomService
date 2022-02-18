using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clean.Model;
using AutoMapper;
using CleanService.IService;
using CleanService.Service;
using CleanService.DBContext;
using System.Linq.Expressions;

namespace Clean.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IMapper mappper;

        public CompaniesController(ICompanyService companyService, IMapper mappper)
        {
            this.companyService = companyService;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetCompanies([FromQuery] Expression<Func<TagBuilder, bool>>? expression)
        {
            List<Company> companies = (await companyService.GetList()).ToList();
            List<CompanyModel> models = mappper.Map<List<CompanyModel>>(companies);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyModel>> GetCompany(int id)
        {
            Company company =  await companyService.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            CompanyModel model = mappper.Map<CompanyModel>(company);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            //_context.Entry(company).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CompanyExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            if (!await companyService.Update(company))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyModel>> PostCompany(Company company)
        {
            //_context.Companies.Add(company);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetCompanies), new { id = company.CompanyId}, company);
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
            if(!await companyService.Delete(id))
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

