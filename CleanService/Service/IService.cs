using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.Service
{
    public interface IService<T>
    {
        public IEnumerable<T> GetList();
        public T GetById(int id);
        public bool Update(T t);
        public bool Delete(int id);
    }
}
