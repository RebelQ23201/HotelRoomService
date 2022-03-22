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
    [Route("api/Room")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly IRoomService<Room> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Room> accountService, IMapper mappper)
        public RoomsController(IRoomService<Room> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomOutputModel>>> GetRooms(
            [FromQuery] int? id)
        {
            Expression<Func<Room, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.RoomId == id);
            }

            List<Room> rooms = (await service.GetList(filters)).ToList();
            List<RoomOutputModel> models = mappper.Map<List<RoomOutputModel>>(rooms);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomOutputModel>> GetRoom(int id)
        {
            Room room = await service.GetById(id);

            if (room == null)
            {
                return NotFound();
            }

            RoomOutputModel model = mappper.Map<RoomOutputModel>(room);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();
            }

            if (!await service.Update(room))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomOutputModel>> PostRoom([FromBody] RoomPostInputModel roomInput)
        {
            int id = await service.GetTotal();
            Room model = new Room();
            model.RoomId = id + 1;
            model.HotelId = roomInput.HotelId;
            model.Name = roomInput.Name;
            model.RoomTypeId = roomInput.RoomTypeId;
            if (!await service.Create(model))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRooms), new { id = id + 1 }, model);
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

        [Route("Hotel")]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<RoomOutputModel>>> GetRoomByHotelId(int id)
        {
            IEnumerable<Room> rooms = await service.GetRoomByHotelId(id);

            if (rooms == null)
            {
                return NotFound();
            }

            List<RoomOutputModel> models = mappper.Map<List<RoomOutputModel>>(rooms);
            //ServiceOutputModel model = mappper.Map<ServiceOutputModel>(account);

            return models;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        [Route("Hotel")]
        public async Task<IActionResult> EditRoom(int id, int hotelId, RoomInputModel room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();
            }

            Room model = new Room();
            model.RoomId = id;
            model.HotelId = room.HotelId;
            model.Name = room.Name;
            model.RoomTypeId = room.RoomTypeId;

            if (!await service.UpdateByHotel(hotelId, model))
            {
                return NotFound();
            }

            return Content("Update Room id " + id + " Successfully");
        }

        [HttpDelete()]
        [Route("Hotel")]
        public async Task<IActionResult> DeleteByHotel(int id, int hotelId)
        {
            if (!await service.DeleteByHotel(id, hotelId))
            {
                return NotFound();
            }

            return Content("Delete Room id " + id + " Successfully");
        }
    }
}
