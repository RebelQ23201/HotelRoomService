using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IBaseService<T>
    {
        public  Task<IEnumerable<T>> GetList(Expression<Func<T, bool>>? expression, bool? isDeep=false);
        public  Task<int> Count(Expression<Func<T, bool>>? expression);
        public  Task<T> GetById(int id, bool? isDeep=true);
        public  Task<bool> Update(T t);
        public Task<bool> Create(T t);
        public  Task<bool> Delete(int id);
    }
}
