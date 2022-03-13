using CleanService.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IRoomTypeService<T> : IBaseService<T>
    {
        public Task<IEnumerable<T>> GetRoomTypeByHotelId(int roomTypeId);
        public Task<int> GetTotal();
        public Task<bool> DeleteByHotel(int roomTypeId, int hotelId);
        public Task<bool> UpdateByHotel(int hotelId, RoomType r);
    }
}
