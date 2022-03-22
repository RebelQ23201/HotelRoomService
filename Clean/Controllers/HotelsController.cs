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
    
        [Route("api/Hotel")]
        [ApiController]
        public class HotelsController : Controller
        {
            private readonly IHotelService<Hotel> service;
            private readonly IMapper mappper;

            //public CompaniesController(IBaseService<Hotel> accountService, IMapper mappper)
            public HotelsController(IHotelService<Hotel> service, IMapper mappper)
            {
                this.service = service;
                this.mappper = mappper;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<HotelOutputModel>>> GetHotels(
                [FromQuery] int? id, bool? detailed =false)
            {
                Expression<Func<Hotel, bool>> filters = c => true;
                if (id != null)
                {
                    filters = filters.AndAlso(c => c.HotelId == id);
                }

                List<Hotel> hotels = (await service.GetList(filters, detailed)).ToList();
                List<HotelOutputModel> models = mappper.Map<List<HotelOutputModel>>(hotels);
                return models;
            }

            // GET: api/TodoItems/5
            [HttpGet("{id}")]
            public async Task<ActionResult<HotelOutputModel>> GetHotel(int id, bool? detailed =true)
            {
                Hotel account = await service.GetById(id, detailed);

                if (account == null)
                {
                    return NotFound();
                }

                HotelOutputModel model = mappper.Map<HotelOutputModel>(account);

                return model;
            }

            // PUT: api/TodoItems/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutHotel(int id, HotelInputModel hotel)
            {
                if (id != hotel.HotelId)
                {
                    return BadRequest();
                }

                Hotel model = new Hotel();
                model.HotelId = id;
                model.Name = hotel.Name;
                model.Address = hotel.Address;
                model.Phone = hotel.Phone;
                model.Email = hotel.Email;

                if (!await service.Update(model))
                    {
                        return NotFound();
                    }

                return Content("Update success");
            }

            // POST: api/TodoItems
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<HotelOutputModel>> PostHotel(Hotel hotel)
            {
                if (!await service.Create(hotel))
                {
                    return NotFound();
                }
                return CreatedAtAction(nameof(GetHotels), new { id = hotel.HotelId }, hotel);
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
