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
        public async Task<ActionResult<IEnumerable<OrderOutputModel>>> GetOrders(
            [FromQuery] int? OrderId, int? HotelId, int? CompanyId, string Name,
            DateTime? StartDate, DateTime? EndDate, DateTime? isActiveDate,
            int? Status, bool? detailed = false)
        {
            Expression<Func<Order, bool>> filters = c => true;
            if (OrderId != null)
            {
                filters = filters.AndAlso(c => c.OrderId == OrderId);
            }
            if (HotelId != null)
            {
                filters = filters.AndAlso(c => c.HotelId == HotelId);
            }
            if (CompanyId != null)
            {
                filters = filters.AndAlso(c => c.OrderId == CompanyId);
            }
            if (!String.IsNullOrWhiteSpace(Name))
            {
                filters = filters.AndAlso(c => c.Name == Name);
            }
            if (StartDate != null)
            {
                filters = filters.AndAlso(c => c.StartDate == StartDate);
            }
            if (EndDate != null)
            {
                filters = filters.AndAlso(c => c.EndDate == EndDate);
            }
            if (isActiveDate != null)
            {
                filters = filters.AndAlso(c => c.StartDate <= isActiveDate && c.EndDate >= isActiveDate);
            }
            if (Status != null)
            {
                filters = filters.AndAlso(c => c.Status == Status);
            }
            List<Order> accounts = (await service.GetList(filters, detailed)).ToList();
            List<OrderOutputModel> models = mappper.Map<List<OrderOutputModel>>(accounts);
            return models;
        }
        [HttpGet("Ids")]
        public async Task<ActionResult<IEnumerable<int>>> GetIds(
           [FromQuery] int? OrderId, int? HotelId, int? CompanyId, string Name,
           DateTime? StartDate, DateTime? EndDate, DateTime? isActiveDate,
           int? Status, bool? detailed = false)
        {
            Expression<Func<Order, bool>> filters = c => true;
            if (OrderId != null)
            {
                filters = filters.AndAlso(c => c.OrderId == OrderId);
            }
            if (HotelId != null)
            {
                filters = filters.AndAlso(c => c.HotelId == HotelId);
            }
            if (CompanyId != null)
            {
                filters = filters.AndAlso(c => c.OrderId == CompanyId);
            }
            if (!String.IsNullOrWhiteSpace(Name))
            {
                filters = filters.AndAlso(c => c.Name == Name);
            }
            if (StartDate != null)
            {
                filters = filters.AndAlso(c => c.StartDate == StartDate);
            }
            if (EndDate != null)
            {
                filters = filters.AndAlso(c => c.EndDate == EndDate);
            }
            if (isActiveDate != null)
            {
                filters = filters.AndAlso(c => c.StartDate <= isActiveDate && c.EndDate >= isActiveDate);
            }
            if (Status != null)
            {
                filters = filters.AndAlso(c => c.Status == Status);
            }
           var accounts = (await service.GetList(filters, detailed));
            List<int> models = accounts.Select(a=>a.OrderId).ToList();
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderOutputModel>> GetOrder(int id, bool? detailed =true)
        {
            Order account = await service.GetById(id, detailed);

            if (account == null)
            {
                return NotFound();
            }

            OrderOutputModel model = mappper.Map<OrderOutputModel>(account);

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
        [HttpPut("Status/{id}")]
        public async Task<IActionResult> PutStatus(int id, int companyId, int status)
        {
            Order order = await service.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            if (order.CompanyId != companyId)
            {
                return Unauthorized();
            }
            order.Status = status;

            if (!await service.Update(order))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderOutputModel>> PostOrder(Order account)
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
