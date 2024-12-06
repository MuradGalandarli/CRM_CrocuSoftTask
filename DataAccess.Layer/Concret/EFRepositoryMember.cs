using DataAccess.Layer.Abstract;
using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Concret
{
    public class EFRepositoryMember : IMember
    {
        public Task<bool> Add(Member t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Member t)
        {
            throw new NotImplementedException();
        }
    }
}
