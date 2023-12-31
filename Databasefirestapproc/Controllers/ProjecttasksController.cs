﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Databasefirestapproc.Context;
using Databasefirestapproc.Models;

namespace Databasefirestapproc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjecttasksController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public ProjecttasksController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Projecttasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projecttask>>> GetProjecttasks()
        {
          if (_context.Projecttasks == null)
          {
              return NotFound();
          }
            return await _context.Projecttasks.ToListAsync();
        }

        // GET: api/Projecttasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projecttask>> GetProjecttask(int id)
        {
          if (_context.Projecttasks == null)
          {
              return NotFound();
          }
            var projecttask = await _context.Projecttasks.FindAsync(id);

            if (projecttask == null)
            {
                return NotFound();
            }

            return projecttask;
        }

        // PUT: api/Projecttasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjecttask(int id, Projecttask projecttask)
        {
            if (id != projecttask.Id)
            {
                return BadRequest();
            }

            _context.Entry(projecttask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjecttaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(projecttask);
        }

        // POST: api/Projecttasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projecttask>> PostProjecttask(Projecttask projecttask)
        {
          if (_context.Projecttasks == null)
          {
              return Problem("Entity set 'ProjectDBContext.Projecttasks'  is null.");
          }
            _context.Projecttasks.Add(projecttask);
            await _context.SaveChangesAsync();

            return Ok(projecttask);
        }

        // DELETE: api/Projecttasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjecttask(int id)
        {
            if (_context.Projecttasks == null)
            {
                return NotFound();
            }
            var projecttask = await _context.Projecttasks.FindAsync(id);
            if (projecttask == null)
            {
                return NotFound();
            }

            _context.Projecttasks.Remove(projecttask);
            await _context.SaveChangesAsync();

            return Ok(projecttask);
        }
        [HttpGet]
        [Route("listtask/{id}")]
        public async Task<ActionResult<IEnumerable<Projecttask>>> listtask([FromRoute] int id)
        {
            return Ok(_context.Projecttasks.Where(p => p.Moduleid == id ).ToList());
        }
        [HttpGet]
        [Route("mytask/{employeeid}")]
        public async Task<ActionResult<IEnumerable<Projecttask>>> mytasks([FromRoute] int employeeid)
        {
            return Ok(_context.Projecttasks.Include(pt=> pt.Project).Include(pt=> pt.Module).Where(m=> m.Employeeid == employeeid).ToList());
        }

        [HttpPut]
        [Route("updatestetus/{id}/{stetus}")]
        public async Task<ActionResult<IEnumerable<Projecttask>>> update([FromRoute]int id, [FromRoute] string stetus)
        {
            Projecttask projecttask = await _context.Projecttasks.FindAsync(id);
            if (projecttask != null)
            {
                projecttask.Stetus = stetus;
                _context.Entry(projecttask).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            return Ok(projecttask); 

        }
        [HttpGet]
        [Route("workreport")]

        public async Task<ActionResult<IEnumerable<Projecttask>>> report()
        {
            var reportdata = (from pt in _context.Projecttasks
                              join e in _context.Employees on pt.Employeeid equals e.Id
                              join p in _context.Projects on pt.Projectid equals p.Id
                              join m in _context.Projectmodules on pt.Moduleid equals m.Id

                              select new
                              {
                                  pt,
                                  Employeename = e.Name,
                                  projectname = p.Name,
                                  modulename = m.Name,
                                  projecttasks = pt.Task,
                                  projectstatus= pt.Stetus

                              }
                               ).ToList();

            return Ok(reportdata);
         }

        private bool ProjecttaskExists(int id)
        {
            return (_context.Projecttasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
