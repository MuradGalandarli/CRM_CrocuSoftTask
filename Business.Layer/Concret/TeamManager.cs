using Business.Layer.Abstract;
using DataAccess.Layer.Abstract;
using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Concret
{
    public class TeamManager:ITeamService
    {
        private readonly ITeam _team;
        public TeamManager(ITeam team)
        {
            _team = team;
        }
        public async Task<(bool, int StatusCode)> Add(Team t)
        {
            var IsSuccess = await _team.Add(t);
            return IsSuccess;
        }

        public async Task<(bool, int StatusCode)> Delete(int id)
        {
            var IsSuccess = await _team.Delete(id);
            return IsSuccess;
        }

        public async Task<(List<Team>, int StatusCode)> GetAll()
        {
            var data = await _team.GetAll();
            return data;
        }

        public async Task<(Team, int StatusCode)> GetById(int id)
        {
            var data = await _team.GetById(id);
            return data;
        }

        public async Task<(bool, int StatusCode)> Update(Team t)
        {
            var IsSuccess = await _team.Update(t);
            return IsSuccess;
        }
    }
}
