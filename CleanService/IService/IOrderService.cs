using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IOrderService<T>: IBaseService<T>
    {
        public Task<int> GetTotal();
        public Task<double> GetTotalMoney(int orderId);
    }
}
