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
    [Route("api/RoomOrder")]
    [ApiController]
    public class RoomOrdersController : Controller
    {
        private readonly IBaseService<RoomOrder> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<RoomOrder> accountService, IMapper mappper)
        public RoomOrdersController(IBaseService<RoomOrder> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomOrderModel>>> GetRoomOrders(
            [FromQuery] int? id)
        {
            Expression<Func<RoomOrder, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.RoomOrderId == id);
            }

            List<RoomOrder> accounts = (await service.GetList(filters)).ToList();
            List<RoomOrderModel> models = mappper.Map<List<RoomOrderModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomOrderModel>> GetRoomOrder(int id)
        {
            RoomOrder account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            RoomOrderModel model = mappper.Map<RoomOrderModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomOrder(int id, RoomOrder account)
        {
            if (id != account.RoomOrderId)
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
        public async Task<ActionResult<RoomOrderModel>> PostRoomOrder(RoomOrder account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRoomOrders), new { id = account.RoomOrderId }, account);
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
