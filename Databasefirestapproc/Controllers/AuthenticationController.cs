using Databasefirestapproc.Context;
using Databasefirestapproc.DTOs;
using Databasefirestapproc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Databasefirestapproc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        ProjectDBContext _context;

        public AuthenticationController(ProjectDBContext projectDBContext)
        {
            _context = projectDBContext;
        }

        [HttpPost]
        [Route("login")]

        public async Task<ActionResult<IEnumerable<Employee>>> login(EmployeeDTO employee)
        {
            IList<Employee> employees = _context.Employees.Where(e=>e.Email ==  employee.Email && e.Password == employee.Password).ToList();
            return Ok(employees);
        }


    }
}
