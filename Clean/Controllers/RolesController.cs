using AutoMapper;
using Clean.Model;
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
    [Route("api/Role")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly IBaseService<Role> roleService;
        //private readonly ICompanyService companyService;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Company> companyService, IMapper mappper)
        public RolesController(IBaseService<Role> roleService, IMapper mappper)
        {
            this.roleService = roleService;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetCompanies(
            [FromQuery] int? id, string name)
        {
            Expression<Func<Role, bool>> filters = r => true;
            if (id != null)
            {
                filters = filters.AndAlso(r => r.RoleId == id);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                filters = filters.AndAlso(r => r.RoleName == name);
            }
            List<Role> roles = (await roleService.GetList(filters)).ToList();
            List<RoleModel> models = mappper.Map<List<RoleModel>>(roles);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetRole(int id)
        {
            Role role = await roleService.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            RoleModel model = mappper.Map<RoleModel>(role);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.RoleId)
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

            if (!await roleService.Update(role))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleModel>> PostRole(Role role)
        {
            //_context.Companies.Add(company);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            if (!await roleService.Create(role))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, role);
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
            if (!await roleService.Delete(id))
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
