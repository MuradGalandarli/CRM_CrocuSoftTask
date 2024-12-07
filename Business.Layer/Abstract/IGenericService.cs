using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        public Task<T> GetById(int id);
        public Task<List<T>> GetAll();
        public Task<bool> Add(T t);
        public Task<bool> Update(T t);
        public Task<bool> Delete(int id);

    }
}
