using AutoMapper;
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
    [Route("api/Service")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly IServiceService<Service> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Service> accountService, IMapper mappper)
        public ServicesController(IServiceService<Service> service, IMapper mappper)
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
        public async Task<IActionResult> PutService(int id, ServiceInputModel serviceInput)
        {
            if (id != serviceInput.ServiceId)
            {
                return BadRequest();
            }

            Service model = new Service();
            model.ServiceId = serviceInput.ServiceId;
            model.CompanyId = serviceInput.CompanyId;
            model.Name = serviceInput.Name;
            model.Price = serviceInput.Price;

            if (!await service.Update(model))
            {
                return NotFound();
            }

            return Content("Update Service " + id + " Successfully");
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceOutputModel>> PostService([FromBody] ServicePOSTInputModel serviceInput)
        {
            int id = await service.GetTotal();
            Service model = new Service();
            model.ServiceId = id + 1;
            model.CompanyId = serviceInput.CompanyId;
            model.Name = serviceInput.Name;
            model.Price = serviceInput.Price;
            if (!await service.Create(model))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetServices), new { id = id }, model);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            if (!await service.Delete(id))
            {
                return NotFound();
            }

            return Content("Delete Service " + id + " Successfully");
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("")]
        public async Task<IActionResult> DeleteByCompany(int id, int companyId)
        {
            if (!await service.DeleteByCompany(id, companyId))
            {
                return NotFound();
            }

            return Content("Delete Service " + id +" Successfully");
        }

        [Route("Comapny")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ServiceOutputModel>>> GetServiceByCompanyId(int id)
        {
            IEnumerable<Service> accounts = await service.GetServiceByCompanyId(id);

            if (accounts == null)
            {
                return NotFound();
            }

            List<ServiceOutputModel> models = mappper.Map<List<ServiceOutputModel>>(accounts);
            //ServiceOutputModel model = mappper.Map<ServiceOutputModel>(account);

            return models;
        }

        [HttpPut()]
        [Route("Company")]
        public async Task<IActionResult> EditService(int id, int companyId, ServiceInputModel serviceInput)
        {
            if (id != serviceInput.ServiceId)
            {
                return BadRequest();
            }

            Service model = new Service();
            model.ServiceId = serviceInput.ServiceId;
            model.CompanyId = serviceInput.CompanyId;
            model.Name = serviceInput.Name;
            model.Price = serviceInput.Price;

            if (!await service.UpdateByCompany(companyId, model))
            {
                return NotFound();
            }

            return Content("Update Service " + id + " Successfully");
        }
    }
}
