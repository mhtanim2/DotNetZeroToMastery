using Microsoft.EntityFrameworkCore;
using PandaIT.Data;
using PandaIT.Dto;
using PandaIT.Interfaces;
using PandaIT.Models;

namespace PandaIT.Repository
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context )
        {
            _context = context;
        }

        public bool CreateProject(ProjectDto projectDto)
        {
            Project ob= new Project();
            ob.ProjectName = "New One";
            _context.Projects.Add( ob );
            return Save();
        }

        public bool DeleteProject(Project project)
        {
            throw new NotImplementedException();
        }

        public Project GetProject(int id)
        {
            return _context.Projects.Where(e => e.ProjectId == id).FirstOrDefault();
        }

        public IEnumerable<Project> GetProjectss()
        {
            return _context.Projects.ToList();

        }

        public Project GetProjectTrimToUpper(DepartmentDto departmentCreate)
        {
            throw new NotImplementedException();
        }

        public bool ProjectExists(int projId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            int ok = _context.SaveChanges();
            return ok>0? true: false;
        }

        public bool UpdateProject(int projId, ProjectDto projectDto)
        {
            throw new NotImplementedException();
        }
    }
}
