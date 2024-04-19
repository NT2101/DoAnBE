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
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<topicType>>> GettopicType()
        {
            return await _context.topicType.ToListAsync();
        }

        // GET: api/topicTypes/5
        [HttpGet]
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
        [HttpPut("Update")]
        public IActionResult UpdateTopicType(int id, TopicTypeDTO Updatetopictype)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tìm teacher cần sửa theo TeacherID
                    var TopicTypeToUpdate = _context.topicType.FirstOrDefault(t => t.ID == id);

                    if (TopicTypeToUpdate == null)
                    {
                        return NotFound($"Teacher with ID '{id}' not found.");
                    }

                    // Cập nhật thông tin của teacher từ DTO
                    TopicTypeToUpdate.Name = Updatetopictype.name;
                    TopicTypeToUpdate.Description = Updatetopictype.description;
   

                    // Lưu các thay đổi vào DbContext
                    _context.SaveChanges();

                    transaction.Commit(); // Lưu thay đổi vào database

                    return Ok($"TopicType with ID '{id}' updated successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Nếu có lỗi, rollback giao dịch
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }
        }

        // POST: api/topicTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost("Create")]
        public async Task<ActionResult<topicType>> CreateTopicType(topicType topicType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.topicType.Add(topicType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopicType", new { id = topicType.ID }, topicType);
        }

        // DELETE: api/topicTypes/5
        [HttpDelete("Delete")]
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
