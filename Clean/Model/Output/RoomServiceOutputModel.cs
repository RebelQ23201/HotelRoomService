using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class RoomServiceOutputModel
    {
        public int? ServiceId { get; set; }
        public int? SystemRoomTypeId { get; set; }

        public virtual ServiceOutputModel Service { get; set; }
        public virtual SystemRoomTypeOutputModel SystemRoomType { get; set; }
    }
}
