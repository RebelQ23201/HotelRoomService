using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IOrderDetailService<T> : IBaseService<T>
    {
        public Task<int> GetTotal();
    }
}
