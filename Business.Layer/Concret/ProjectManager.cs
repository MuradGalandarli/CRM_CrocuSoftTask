using Business.Layer.Abstract;
using DataAccess.Layer.Abstract;
using Entity.Layer;
using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Concret
{
    public class ProjectManager:IProjectService
    {
        private readonly IProject _project;
        public ProjectManager(IProject project)
        {
            _project = project;
        }
        public async Task<bool> Add(Project t)
        {
            var IsSuccess = await _project.Add(t);
            return IsSuccess;
        }

        public async Task<bool> Delete(int id)
        {
            var IsSuccess = await _project.Delete(id);
            return IsSuccess;
        }

        public async Task<List<Project>> GetAll()
        {
            var data = await _project.GetAll();
            return data;
        }

        public async Task<Project> GetById(int id)
        {
            var data = await _project.GetById(id);
            return data;
        }

        public async Task<bool> Update(Project t)
        {
            var IsSuccess = await _project.Update(t);
            return IsSuccess;
        }
    }
}
