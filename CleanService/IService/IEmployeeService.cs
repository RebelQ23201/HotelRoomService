using CleanService.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IEmployeeService<T> : IBaseService<T>
    {
        public Task<IEnumerable<T>> GetEmployeeByCompanyId(int companyId);
        public Task<int> GetTotal();
        public Task<bool> DeleteByCompany(int employee, int companyId);
        public Task<bool> UpdateByCompany(int companyId, Employee c);
    }
}
