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
    [Route("api/SystemRoomType")]
    [ApiController]
    public class SystemRoomTypesController : Controller
    {
        private readonly ISystemRoomTypeService<SystemRoomType> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<SystemRoomType> accountService, IMapper mappper)
        public SystemRoomTypesController(ISystemRoomTypeService<SystemRoomType> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemRoomTypeOutputModel>>> GetSystemRoomTypes(
            [FromQuery] int? id)
        {
            Expression<Func<SystemRoomType, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.SystemRoomTypeId == id);
            }

            List<SystemRoomType> accounts = (await service.GetList(filters)).ToList();
            List<SystemRoomTypeOutputModel> models = mappper.Map<List<SystemRoomTypeOutputModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemRoomTypeOutputModel>> GetSystemRoomType(int id)
        {
            SystemRoomType account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            SystemRoomTypeOutputModel model = mappper.Map<SystemRoomTypeOutputModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSystemRoomType(int id, SystemRoomTypeInputModel account)
        {
            if (id != account.SystemRoomTypeId)
            {
                return BadRequest();
            }

            SystemRoomType roomtype = new SystemRoomType();
            roomtype.SystemRoomTypeId = account.SystemRoomTypeId;
            roomtype.Name = account.Name;

            if (!await service.Update(roomtype))
            {
                return NotFound();
            }

            return Content("Update Service System Room type id = " + id + " Successfully");
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SystemRoomTypeOutputModel>> PostSystemRoomType([FromBody] SystemRoomTypePOSTInputModel model)
        {
            int id = await service.GetTotal();
            SystemRoomType roomtype = new SystemRoomType();
            roomtype.SystemRoomTypeId = id + 1;
            roomtype.Name = model.Name;

            if (!await service.Create(roomtype))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetSystemRoomTypes), new { id = id + 1 }, roomtype);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            if (!await service.Delete(id))
            {
                return NotFound();
            }

            return Content("Delete Service System Room type id = " + id + " Successfully");
        }
    }
}
