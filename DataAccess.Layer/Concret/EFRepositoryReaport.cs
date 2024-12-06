using DataAccess.Layer.Abstract;
using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Concret
{
    public class EFRepositoryReaport : IReaport 
    {
        public Task<bool> Add(Reaport t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reaport>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Reaport> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Reaport t)
        {
            throw new NotImplementedException();
        }
    }
}
