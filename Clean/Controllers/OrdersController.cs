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
    [Route("api/Order")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService<Order> service;
        private readonly IRoomOrderService<RoomOrder> roomOrderService;
        private readonly IOrderDetailService<OrderDetail> orderDetailService;
        private readonly IServiceService<Service> serviceService;
        private readonly IEmployeeService<Employee> employeeService;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Order> accountService, IMapper mappper)
        public OrdersController(IOrderService<Order> service, IRoomOrderService<RoomOrder> roomOrderService, IOrderDetailService<OrderDetail> orderDetailService, IServiceService<Service> serviceService, IEmployeeService<Employee> employeeService ,IMapper mappper)
        {
            this.service = service;
            this.roomOrderService = roomOrderService;
            this.orderDetailService = orderDetailService;
            this.serviceService = serviceService;
            this.employeeService = employeeService;
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
                filters = filters.AndAlso(c => c.CompanyId == CompanyId);
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

            return Ok("update successfully");
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

        [HttpPost]
        [Route("Booking")]
        public async Task<ActionResult<OrderOutputModel>> PostOrderWithDetail(OrderInputModel order)
        {
            DateTime startDate = (DateTime)order.StartDate;
            DateTime endDate = (DateTime)order.EndDate;

            int totalDays = (endDate.Date - startDate.Date).Days + 1;

            int id = await service.GetTotal();
            Order modelOrder = new Order();
            modelOrder.OrderId = id + 1;
            modelOrder.Name = order.OrderName;
            modelOrder.HotelId = order.HotelId;
            modelOrder.CompanyId = order.CompanyId;
            modelOrder.StartDate = order.StartDate;
            modelOrder.EndDate = order.EndDate;
            modelOrder.Status = 0;
            if (!await service.Create(modelOrder))
            {
                return NotFound();
            }

            

            foreach (int roomId in order.roomList)
            {
                int roomOrderId = await roomOrderService.GetTotal();
                RoomOrder roomOrder = new RoomOrder();
                roomOrder.RoomOrderId = roomOrderId + 1;
                roomOrder.RoomId = roomId;
                roomOrder.OrderId = modelOrder.OrderId;
                if (!await roomOrderService.Create(roomOrder))
                {
                    return NotFound();
                }
                foreach (int serviceId in order.serviceList)
                {
                    int orderDetailId = await orderDetailService.GetTotal();
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderDetailId = orderDetailId + 1;
                    orderDetail.RoomOrderId = roomOrder.RoomOrderId;
                    
                    orderDetail.ServiceId = serviceId;
                    Service service = await serviceService.GetById(serviceId);
                    orderDetail.Price = service.Price;
                    orderDetail.Quantity = totalDays;

                    Random random = new Random();
                    List<Employee> list = (await employeeService.GetEmployeeByCompanyId((int)order.CompanyId)).ToList();
                    int randomEmployee = random.Next(0, list.Count());

                    orderDetail.EmployeeId = list[randomEmployee].EmployeeId;
                    if (!await orderDetailService.Create(orderDetail))
                    {
                        return NotFound();
                    }
                }
            }
            return CreatedAtAction(nameof(GetOrders), new { id = id + 1 }, modelOrder);
        }
    }
}
