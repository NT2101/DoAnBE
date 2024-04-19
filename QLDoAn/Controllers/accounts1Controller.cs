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
    public class accounts1Controller : ControllerBase
    {
        private readonly DataContext _context;

        public accounts1Controller(DataContext context)
        {
            _context = context;
        }

        // GET: api/accounts1
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<account>>> Getaccount()
        {
            return await _context.account.ToListAsync();
        }

        // GET: api/accounts1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<account>> Getaccount(int id)
        {
            var account = await _context.account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/accounts1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> Putaccount(int id, account account)
        {
            if (id != account.ID)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!accountExists(id))
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

        // POST: api/accounts1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<account>> Postaccount(account account)
        {
            _context.account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaccount", new { id = account.ID }, account);
        }

        // DELETE: api/accounts1/5
        [HttpDelete("delete")]
        public async Task<IActionResult> Deleteaccount(int id)
        {
            var account = await _context.account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.account.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool accountExists(int id)
        {
            return _context.account.Any(e => e.ID == id);
        }
    }
}
