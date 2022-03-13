using CleanService.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IRoomService<T> : IBaseService<T>
    {
        public Task<IEnumerable<T>> GetRoomByHotelId(int companyId);
        public Task<int> GetTotal();
        public Task<bool> DeleteByHotel(int roomId, int hotelId);
        public Task<bool> UpdateByHotel(int hotelId, Room r);
    }
}
