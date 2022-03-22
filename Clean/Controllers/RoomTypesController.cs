using AutoMapper;
using Clean.Filter;
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
    [Route("api/RoomType")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class RoomTypesController : Controller
    {
        private readonly IRoomTypeService<RoomType> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<RoomType> accountService, IMapper mappper)
        public RoomTypesController(IRoomTypeService<RoomType> service, IMapper mappper)
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

            List<RoomType> roomTypes = (await service.GetList(filters)).ToList();
            List<RoomTypeOutputModel> models = mappper.Map<List<RoomTypeOutputModel>>(roomTypes);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeOutputModel>> GetRoomType(int id)
        {
            RoomType roomType = await service.GetById(id);

            if (roomType == null)
            {
                return NotFound();
            }

            RoomTypeOutputModel model = mappper.Map<RoomTypeOutputModel>(roomType);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomType(int id, RoomType roomType)
        {
            if (id != roomType.RoomTypeId)
            {
                return BadRequest();
            }

            if (!await service.Update(roomType))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomTypeOutputModel>> PostRoomType([FromBody] RoomTypePostInputModel roomTypeInput)
        {
            int id = await service.GetTotal();
            RoomType model = new RoomType();
            model.RoomTypeId = id + 1;
            model.HotelId = roomTypeInput.HotelId;
            model.Name = roomTypeInput.Name;
            model.SystemRoomTypeId = roomTypeInput.SystemRoomTypeId;
            if (!await service.Create(model))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRoomTypes), new { id = id + 1 }, model);
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
        public async Task<ActionResult<IEnumerable<RoomTypeOutputModel>>> GetRoomTypeByHotelId(int id)
        {
            IEnumerable<RoomType> roomTypes = await service.GetRoomTypeByHotelId(id);

            if (roomTypes == null)
            {
                return NotFound();
            }

            List<RoomTypeOutputModel> models = mappper.Map<List<RoomTypeOutputModel>>(roomTypes);
            //ServiceOutputModel model = mappper.Map<ServiceOutputModel>(account);

            return models;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        [Route("Hotel")]
        public async Task<IActionResult> EditRoom(int id, int hotelId, RoomTypeInputModel roomType)
        {
            if (id != roomType.RoomTypeId)
            {
                return BadRequest();
            }

            RoomType model = new RoomType();
            model.RoomTypeId = id;
            model.HotelId = roomType.HotelId;
            model.Name = roomType.Name;
            model.SystemRoomTypeId = roomType.SystemRoomTypeId;

            if (!await service.UpdateByHotel(hotelId, model))
            {
                return NotFound();
            }

            return Content("Update Room type id " + id + " Successfully");
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
