using Clean.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class OrderOutputModel
    {
        public int OrderId { get; set; }
        public int? HotelId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime? StartDate { get; set; }
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? CompanyId { get; set; }

        public virtual CompanyOutputModel Company { get; set; }
        public virtual HotelOutputModel Hotel { get; set; }
        public virtual List<RoomOrderOutputModel> RoomOrders { get; set; }
    }
}
