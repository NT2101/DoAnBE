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
    public class topicTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public topicTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/topicTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<topicType>>> GettopicType()
        {
            return await _context.topicType.ToListAsync();
        }

        // GET: api/topicTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<topicType>> GettopicType(int id)
        {
            var topicType = await _context.topicType.FindAsync(id);

            if (topicType == null)
            {
                return NotFound();
            }

            return topicType;
        }

        // PUT: api/topicTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttopicType(int id, topicType topicType)
        {
            if (id != topicType.ID)
            {
                return BadRequest();
            }

            _context.Entry(topicType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!topicTypeExists(id))
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

        // POST: api/topicTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<topicType>> PosttopicType(topicType topicType)
        {
            _context.topicType.Add(topicType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettopicType", new { id = topicType.ID }, topicType);
        }

        // DELETE: api/topicTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetopicType(int id)
        {
            var topicType = await _context.topicType.FindAsync(id);
            if (topicType == null)
            {
                return NotFound();
            }

            _context.topicType.Remove(topicType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool topicTypeExists(int id)
        {
            return _context.topicType.Any(e => e.ID == id);
        }
    }
}
