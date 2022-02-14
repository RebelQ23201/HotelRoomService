﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int? HotelId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? CompanyId { get; set; }

        public virtual CompanyModel Company { get; set; }
        public virtual HotelModel Hotel { get; set; }
        public virtual List<RoomOrderModel> RoomOrders { get; set; }
    }
}
