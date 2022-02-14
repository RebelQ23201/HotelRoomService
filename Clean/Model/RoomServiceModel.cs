using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class RoomServiceModel
    {
        public int? ServiceId { get; set; }
        public int? SystemRoomTypeId { get; set; }

        public virtual ServiceModel Service { get; set; }
        public virtual SystemRoomTypeModel SystemRoomType { get; set; }
    }
}
