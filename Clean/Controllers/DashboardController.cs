using CleanService.DBContext;
using CleanService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IHotelService<Hotel> HotelService;
        private readonly ICompanyService<Company> CompanyService;
        private readonly IOrderService<Order> OrderService;

        public DashboardController(IHotelService<Hotel> hotelService, ICompanyService<Company> companyService, IOrderService<Order> orderService)
        {
            HotelService = hotelService;
            CompanyService = companyService;
            OrderService = orderService;
        }
        [HttpGet("admin")]
        public async Task<ActionResult<Object>> GetAdminPageInfo()
        {
            int hotelCount = await HotelService.Count((h=>true));
            int companyCount = await CompanyService.Count((c=>true));
            int countAllOrder = await OrderService.Count((o=>true));
            DateTime today = DateTime.Now.Date;
            int countTodayOrder = await OrderService.Count((c => c.StartDate.Value.Date <= today && c.EndDate.Value.Date >= today));
            return new { hotel = hotelCount, company = companyCount, todayOrders = countTodayOrder, totalOrders = countAllOrder };
        }

        [HttpGet("admin/company/{id}")]
        public async Task<ActionResult<Object>> GetCompanyOrder(int id)
        {
            int countAllOrder = await OrderService.Count((o => o.CompanyId == id));
            DateTime today = DateTime.Now.Date;
            int countTodayOrder = (await OrderService.GetList((c => c.StartDate.Value.Date <= today && c.EndDate.Value.Date >= today && c.CompanyId == id))).Count();
            return new { todayOrders = countTodayOrder, totalOrders = countAllOrder };
        }
    }
}
