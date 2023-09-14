using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMAWS_T2204M_TranHung.Models;
using AutoMapper;
using DMAWS_T2204M_TranHung.DTOs;
using DMAWS_T2204M_TranHung.ViewModels;

namespace DMAWS_T2204M_TranHung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public ProjectsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects(
            string? projectName = null,
            bool inProgress = false,
            bool finished = false)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }

            var query = _context.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(projectName))
            {
                query = query.Where(p => p.ProjectName.Contains(projectName));
            }

            var currentTime = DateTime.UtcNow;

            if (inProgress)
            {
                query = query.Where(p => p.ProjectEndDate == null || p.ProjectEndDate >= currentTime);
            }

            if (finished)
            {
                query = query.Where(p => p.ProjectEndDate != null && p.ProjectEndDate < currentTime);
            }

            var projects = await query.ToListAsync();
            var projectDTOs = _mapper.Map<List<ProjectDTO>>(projects);
            return projectDTOs;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectEmployees)
                .ThenInclude(pe => pe.Employees)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            var projectDTO = _mapper.Map<ProjectDTO>(project);

            return projectDTO;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> PostProject(ProjectCreateModel projectCreateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = _mapper.Map<Project>(projectCreateModel);

            // Custom validation: Check if ProjectEndDate is not null and ProjectStartDate is greater than or equal to ProjectEndDate
            if (project.ProjectEndDate.HasValue && project.ProjectStartDate >= project.ProjectEndDate)
            {
                ModelState.AddModelError("ProjectEndDate", "ProjectEndDate must be greater than ProjectStartDate.");
                return BadRequest(ModelState);
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var createdProjectDTO = _mapper.Map<ProjectDTO>(project);

            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, createdProjectDTO);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
