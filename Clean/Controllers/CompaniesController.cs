using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clean.Model;
using Clean.DataContext;
using Clean.Mapper;
using AutoMapper;
using Clean.Service;
using Clean.Repository;

namespace Clean.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly IMapper mappper;
        private CompanyService companyService;

        public CompaniesController(IMapper mappper)
        {
            companyService = new CompanyService();
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetCompanies()
        {
            var companies = await companyService.getCompanies();
            List<CompanyModel> model = mappper.Map<List<CompanyModel>>(companies);
            return model;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyModel>> GetCompanyById(int id)
        {
            var company = await companyService.GetCompany(id);
            CompanyModel model = mappper.Map<CompanyModel>(company);
            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditCompany(int id, Company company)
        {
            var message = await companyService.editCompany(id, company);
            return message;
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyModel>> AddCompany(Company company)
        {
            var companyAdded = await companyService.addCompany(company);
            CompanyModel model = mappper.Map<CompanyModel>(company);

            return model;
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteTodoItem(int id)
        {
            var message = await companyService.deleteCompany(id);
            return message;
        }
    }
}

