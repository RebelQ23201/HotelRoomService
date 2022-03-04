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
    [Route("api/RoomType")]
    [ApiController]
    public class RoomTypesController : Controller
    {
        private readonly IBaseService<RoomType> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<RoomType> accountService, IMapper mappper)
        public RoomTypesController(IBaseService<RoomType> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeOutputModel>>> GetRoomTypes(
            [FromQuery] int? id)
        {
            Expression<Func<RoomType, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.RoomTypeId == id);
            }

            List<RoomType> accounts = (await service.GetList(filters)).ToList();
            List<RoomTypeOutputModel> models = mappper.Map<List<RoomTypeOutputModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeOutputModel>> GetRoomType(int id)
        {
            RoomType account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            RoomTypeOutputModel model = mappper.Map<RoomTypeOutputModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomType(int id, RoomType account)
        {
            if (id != account.RoomTypeId)
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
        public async Task<ActionResult<RoomTypeOutputModel>> PostRoomType(RoomType account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRoomTypes), new { id = account.RoomTypeId }, account);
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
