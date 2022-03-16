using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Input
{
    public class OrderInputModel
    {
        public int? HotelId { get; set; }
        public string OrderName { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> roomList { get; set; }
        public List<int> serviceList { get; set; }
    }
}
