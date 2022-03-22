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
    [Route("api/OrderDetail")]
    [ApiController]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailService<OrderDetail> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<OrderDetail> accountService, IMapper mappper)
        public OrderDetailsController(IOrderDetailService<OrderDetail> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailOutputModel>>> GetOrderDetails(
            [FromQuery] int? id)
        {
            Expression<Func<OrderDetail, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.OrderDetailId == id);
            }

            List<OrderDetail> orderDetails = (await service.GetList(filters)).ToList();
            List<OrderDetailOutputModel> models = mappper.Map<List<OrderDetailOutputModel>>(orderDetails);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailOutputModel>> GetOrderDetail(int id)
        {
            OrderDetail account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            OrderDetailOutputModel model = mappper.Map<OrderDetailOutputModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest();
            }

            if (!await service.Update(orderDetail))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetailOutputModel>> PostOrderDetail(OrderDetail orderDetail)
        {
            if (!await service.Create(orderDetail))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetOrderDetails), new { id = orderDetail.OrderDetailId }, orderDetail);
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
