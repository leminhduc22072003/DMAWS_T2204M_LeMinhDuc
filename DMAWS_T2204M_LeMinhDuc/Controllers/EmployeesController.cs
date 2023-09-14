using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMAWS_T2204M_TranHung.Models;
using DMAWS_T2204M_TranHung.DTOs;
using AutoMapper;
using DMAWS_T2204M_TranHung.ViewModels;

namespace DMAWS_T2204M_TranHung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public EmployeesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = _context.Employees.ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }
            
            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);

            return employeeDTOs;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return employeeDTO;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeCreateModel employeeCreateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _mapper.Map<Employee>(employeeCreateModel);

            // Custom validation: Check if the Employee is over 16 years old
            if (!IsEmployeeOver16(employee.EmployeeDOB))
            {
                ModelState.AddModelError("EmployeeDOB", "Employee must be over 16 years old.");
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var createdEmployeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, createdEmployeeDTO);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }

        private bool IsValidDateOfBirth(DateTime dateOfBirth)
        {
            // Add your validation logic here to check if the dateOfBirth is a valid date
            return dateOfBirth != DateTime.MinValue; // Example: Check if it's not the default DateTime value
        }

        private bool IsEmployeeOver16(DateTime dateOfBirth)
        {
            // Add your validation logic here to check if the employee is over 16 years old
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age >= 16;
        }

    }


}
