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
        public async Task<(bool, int StatusCode)> Add(Project t)
        {
            var IsSuccess = await _project.Add(t);
            return IsSuccess;
        }

        public async Task<(bool, int StatusCode)> Delete(int id)
        {
            var IsSuccess = await _project.Delete(id);
            return IsSuccess;
        }

        public async Task<(List<Project>, int StatusCode)> GetAll()
        {
            var data = await _project.GetAll();
            return data;
        }

        public async Task<(Project, int StatusCode)> GetById(int id)
        {
            var data = await _project.GetById(id);
            return data;
        }

        public async Task<(bool, int StatusCode)> Update(Project t)
        {
            var IsSuccess = await _project.Update(t);
            return IsSuccess;
        }
    }
}
