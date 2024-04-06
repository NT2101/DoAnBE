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
    public class teachersController : ControllerBase
    {
        private readonly DataContext _context;

        public teachersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<teacher>>> Getteacher()
        {
            return await _context.teacher.ToListAsync();
        }

        // GET: api/teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<teacher>> Getteacher(int id)
        {
            var teacher = await _context.teacher.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putteacher(int id, teacher teacher)
        {
            if (id != teacher.TeacherID)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!teacherExists(id))
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

        // POST: api/teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<teacher>> Postteacher(teacher teacher)
        {
            _context.teacher.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getteacher", new { id = teacher.TeacherID }, teacher);
        }

        // DELETE: api/teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteteacher(int id)
        {
            var teacher = await _context.teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.teacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool teacherExists(int id)
        {
            return _context.teacher.Any(e => e.TeacherID == id);
        }
    }
}
