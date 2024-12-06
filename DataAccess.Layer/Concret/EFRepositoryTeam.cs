using DataAccess.Layer.Abstract;
using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Concret
{
    public class EFRepositoryTeam : ITeam
    {
        public Task<bool> Add(Team t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Team>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Team t)
        {
            throw new NotImplementedException();
        }
    }
}
