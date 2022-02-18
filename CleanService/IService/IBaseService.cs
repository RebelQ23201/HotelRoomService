using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IBaseService<T>
    {
        public  Task<IEnumerable<T>> GetList();
        public  Task<T> GetById(int id);
        public  Task<bool> Update(T t);
        public  Task<bool> Delete(int id);
    }
}
