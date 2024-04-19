using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDoAn.Models;
using quanlidoan.Data;

namespace QLDoAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly DataContext _context;

        public TopicsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Topics
        [HttpGet("all")]
        public IActionResult GetTopics()
        {
            var topics = _context.Topic
                .Include(t => t.student)
                .Include(t => t.teacher)
                .Include(t => t.topicType)
                .Select(t => new
                {
                    TopicID = t.TopicID,
                    Name = t.Name,
                    Description = t.Description,
                    StudentName = t.student.Name,
                    TeacherName = t.teacher.Name,
                    TopicTypeName = t.topicType.Name
                })
                .ToList();

            return Ok(topics);
        }


        // GET: api/Topics/5
        [HttpGet]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            var topic = await _context.Topic.FindAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }

        // PUT: api/Topics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutTopic(int id, Topic topic)
        {
            if (id != topic.TopicID)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
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

        // POST: api/Topics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost("Create")]
        public async Task<ActionResult<Topic>> PostTopic(TopicInputModel request)
        {
            try
            {
                // Lấy thông tin sinh viên từ request
                var loggedInStudentId = request.StudentID;

                // Kiểm tra xem sinh viên có tồn tại trong database không
                var student = await _context.student.FirstOrDefaultAsync(s => s.StudentID == loggedInStudentId);
                if (student == null)
                {
                    return BadRequest("Sinh viên không tồn tại trong hệ thống.");
                }

                // Tạo một topic mới và gán thông tin từ request
                var newTopic = new Topic
                {
                    StudentID = student.StudentID,
                    Name = request.Name,
                    TeacherID = request.TeacherID,
                    TopicTypeID = request.TopicTypeID,
                    Description = request.Description,
                    TopicYear = request.TopicYear
                };

                // Lưu topic vào cơ sở dữ liệu
                _context.Topic.Add(newTopic);
                await _context.SaveChangesAsync();

                return Ok(newTopic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tạo đề tài: {ex.Message}");
            }
        }


        // DELETE: api/Topics/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _context.Topic.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }

            _context.Topic.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.TopicID == id);
        }
    }
}
