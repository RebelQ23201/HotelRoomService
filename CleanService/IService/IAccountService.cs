using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanService.IService
{
    public interface IAccountService<T> : IBaseService<T>
    {
        public Task<T> GetEmail(string email);

        public Task<bool> CreateViaSignIn(string email);
    }
}
