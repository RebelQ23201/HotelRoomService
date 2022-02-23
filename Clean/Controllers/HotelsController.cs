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
    
        [Route("api/Hotel")]
        [ApiController]
        public class HotelsController : Controller
        {
            private readonly IBaseService<Hotel> service;
            private readonly IMapper mappper;

            //public CompaniesController(IBaseService<Hotel> accountService, IMapper mappper)
            public HotelsController(IBaseService<Hotel> service, IMapper mappper)
            {
                this.service = service;
                this.mappper = mappper;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<HotelModel>>> GetHotels(
                [FromQuery] int? id)
            {
                Expression<Func<Hotel, bool>> filters = c => true;
                if (id != null)
                {
                    filters = filters.AndAlso(c => c.HotelId == id);
                }

                List<Hotel> accounts = (await service.GetList(filters)).ToList();
                List<HotelModel> models = mappper.Map<List<HotelModel>>(accounts);
                return models;
            }

            // GET: api/TodoItems/5
            [HttpGet("{id}")]
            public async Task<ActionResult<HotelModel>> GetHotel(int id)
            {
                Hotel account = await service.GetById(id);

                if (account == null)
                {
                    return NotFound();
                }

                HotelModel model = mappper.Map<HotelModel>(account);

                return model;
            }

            // PUT: api/TodoItems/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutHotel(int id, Hotel account)
            {
                if (id != account.HotelId)
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
            public async Task<ActionResult<HotelModel>> PostHotel(Hotel account)
            {
                if (!await service.Create(account))
                {
                    return NotFound();
                }
                return CreatedAtAction(nameof(GetHotels), new { id = account.HotelId }, account);
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
