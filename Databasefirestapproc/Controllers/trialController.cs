using Databasefirestapproc.Context;
using Databasefirestapproc.DTOs;
using Databasefirestapproc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Databasefirestapproc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class trialController : ControllerBase
    {
        ProjectDBContext _context;
        public trialController(ProjectDBContext projectDBContext)
        {
            _context = projectDBContext;
        }

       
        [HttpGet]
        [Route("addmanegerid")]
        public async Task<ActionResult<IEnumerable<Project>>> addmanegerid()
        {
            return Ok(_context.Projects.Include(p => p.Maneger).ToList());
        }
        [HttpPost]
        [Route("emailpassword")]
        public async Task<ActionResult<IEnumerable<Employee>>> emailpassword(EmployeeDTO employeeDTO)
        {
            return Ok(_context.Employees.Where(e=>e.Email == employeeDTO.Email && e.Password == employeeDTO.Password).ToList());
        }
        [HttpPost]
        [Route("projecttask")]
        public async Task<ActionResult<IEnumerable<Projecttask>>> projecttask(Projecttask projecttask)
        {
           _context.Projecttasks.Add(projecttask);
            _context.SaveChanges();
            return Ok(projecttask);

        }
        [HttpGet]
        [Route("listproject")]
        public async Task<ActionResult<IEnumerable<Project>>> listproject()
        {
            return Ok(_context.Projects.Include(p=>p.Projecttasks).ToList());
        }

       




    }
}
