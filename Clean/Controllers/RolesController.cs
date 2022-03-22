using AutoMapper;
using Clean.Filter;
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
    [Route("api/Role")]
    [ApiController]
    [TokenAuthenticationFilter]
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
        public async Task<ActionResult<IEnumerable<RoleOutputModel>>> GetRoles(
            [FromQuery] int? id)
        {
            Expression<Func<Role, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.RoleId == id);
            }

            List<Role> role = (await service.GetList(filters)).ToList();
            List<RoleOutputModel> models = mappper.Map<List<RoleOutputModel>>(role);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleOutputModel>> GetRole(int id)
        {
            Role role = await service.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            RoleOutputModel model = mappper.Map<RoleOutputModel>(role);

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

            if (!await service.Update(role))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleOutputModel>> PostRole(Role role)
        {
            if (!await service.Create(role))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRoles), new { id = role.RoleId }, role);
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
