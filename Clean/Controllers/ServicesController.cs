using AutoMapper;
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
    [Route("api/Service")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly IBaseService<Service> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Service> accountService, IMapper mappper)
        public ServicesController(IBaseService<Service> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceOutputModel>>> GetServices(
            [FromQuery] int? id)
        {
            Expression<Func<Service, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.ServiceId == id);
            }

            List<Service> accounts = (await service.GetList(filters)).ToList();
            List<ServiceOutputModel> models = mappper.Map<List<ServiceOutputModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceOutputModel>> GetService(int id)
        {
            Service account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            ServiceOutputModel model = mappper.Map<ServiceOutputModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service account)
        {
            if (id != account.ServiceId)
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
        public async Task<ActionResult<ServiceOutputModel>> PostService(Service account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetServices), new { id = account.ServiceId }, account);
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
