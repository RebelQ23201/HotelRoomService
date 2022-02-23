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
    [Route("api/Room")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly IBaseService<Room> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Room> accountService, IMapper mappper)
        public RoomsController(IBaseService<Room> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomModel>>> GetRooms(
            [FromQuery] int? id)
        {
            Expression<Func<Room, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.RoomId == id);
            }

            List<Room> accounts = (await service.GetList(filters)).ToList();
            List<RoomModel> models = mappper.Map<List<RoomModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomModel>> GetRoom(int id)
        {
            Room account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            RoomModel model = mappper.Map<RoomModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room account)
        {
            if (id != account.RoomId)
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
        public async Task<ActionResult<RoomModel>> PostRoom(Room account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRooms), new { id = account.RoomId }, account);
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
