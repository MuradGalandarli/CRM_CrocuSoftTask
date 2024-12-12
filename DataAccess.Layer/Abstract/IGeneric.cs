using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Abstract
{
    public interface IGeneric<T> where T : class
    {
        public Task<(T, int StatusCode)> GetById(int id);
        public Task<(List<T>, int StatusCode)> GetAll();
        public Task<(bool,int StatusCode)> Add(T t);
        public Task<(bool, int StatusCode)> Update(T t);
        public Task<(bool, int StatusCode)> Delete(int id);

    }
}
