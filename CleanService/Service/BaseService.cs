using CleanService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
