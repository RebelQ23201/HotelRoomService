using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IServiceService<T> : IBaseService<T>
    {
        public Task<IEnumerable<T>> GetServiceByCompanyId(int companyId);
        public Task<int> GetTotal();
        public Task<bool> DeleteByCompany(int serviceId, int companyId);
        public Task<bool> UpdateByCompany(int companyId, DBContext.Service s);
    }
}
