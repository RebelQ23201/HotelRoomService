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
        private readonly IBaseService<Role> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Role> accountService, IMapper mappper)
        public RolesController(IBaseService<Role> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRoles(
            [FromQuery] int? id)
        {
            Expression<Func<Role, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.RoleId == id);
            }

            List<Role> accounts = (await service.GetList(filters)).ToList();
            List<RoleModel> models = mappper.Map<List<RoleModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetRole(int id)
        {
            Role account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            RoleModel model = mappper.Map<RoleModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role account)
        {
            if (id != account.RoleId)
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
        public async Task<ActionResult<RoleModel>> PostRole(Role account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRoles), new { id = account.RoleId }, account);
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
