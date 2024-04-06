using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDoAn.Models;
using quanlidoan.Data;

namespace QLDoAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentsController : ControllerBase
    {
        private readonly DataContext _context;

        public studentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<student>>> GetsinhVien()
        {
            return await _context.student.ToListAsync();
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<student>> Getstudent(string id)
        {
            var student = await _context.student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstudent(string id, student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!studentExists(id))
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

        // POST: api/students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<student>> Poststudent(student student)
        {
            _context.student.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (studentExists(student.StudentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getstudent", new { StudentID = student.StudentID }, student);
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestudent(string StudentID)
        {
            var student = await _context.student.FindAsync(StudentID);
            if (student == null)
            {
                return NotFound();
            }

            _context.student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool studentExists(string StudentID)
        {
            return _context.student.Any(e => e.StudentID == StudentID);
        }
    }
}
