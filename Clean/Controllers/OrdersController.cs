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
    [Route("api/Order")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IBaseService<Order> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Order> accountService, IMapper mappper)
        public OrdersController(IBaseService<Order> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders(
            [FromQuery] int? id)
        {
            Expression<Func<Order, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.OrderId == id);
            }

            List<Order> accounts = (await service.GetList(filters)).ToList();
            List<OrderModel> models = mappper.Map<List<OrderModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrder(int id)
        {
            Order account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            OrderModel model = mappper.Map<OrderModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order account)
        {
            if (id != account.OrderId)
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
        public async Task<ActionResult<OrderModel>> PostOrder(Order account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetOrders), new { id = account.OrderId }, account);
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
