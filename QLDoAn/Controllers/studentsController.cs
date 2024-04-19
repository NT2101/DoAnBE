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
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<student>>> GetsinhVien()
        {
            return await _context.student.ToListAsync();
        }

        // GET: api/students/5
        [HttpGet]
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

        // POST: api/students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        public IActionResult CreateStudent(StudentCreateDTO studentDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tạo một account mới
                    var account = new account
                    {
                        Name = studentDto.StudentID, // Sử dụng StudentID làm Name của account
                        Password = studentDto.Dob.ToString("ddMMyyyy"), // Password là MMyyyy của Dob
                        RoleID = 1, // Giả sử RoleID mặc định là 1
                        Status = 1, // Giả sử Status mặc định là 1
                        CreatedDate = DateTime.Now,
                        CreatedUser = "API",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "API"
                    };

                    // Thêm account vào DbContext
                    _context.account.Add(account);
                    _context.SaveChanges(); // Lưu để có được ID của account mới được tạo

                    // Tạo một student và liên kết với account vừa tạo
                    var student = new student
                    {
                        StudentID = studentDto.StudentID,
                        Name = studentDto.Name,
                        Dob = studentDto.Dob,
                        Sex = studentDto.Sex,
                        Address = studentDto.Address,
                        PhoneNumber = studentDto.PhoneNumber,
                        EmailAddress = studentDto.EmailAddress,
                        Country = studentDto.Country,
                        ImageUrl = studentDto.ImageUrl,
                        AccountID = account.ID, // Sử dụng ID của account vừa được tạo
                        Status = 1,
                        CreatedDate = DateTime.Now,
                        CreatedUser = "API",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "API"
                    };

                    // Thêm student vào DbContext
                    _context.student.Add(student);
                    _context.SaveChanges(); // Lưu để hoàn thành việc thêm student

                    transaction.Commit(); // Lưu thay đổi vào database

                    return Ok("Student created successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Nếu có lỗi, rollback giao dịch
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }
        }

            [HttpPut("Update")]
        public IActionResult UpdateStudent(string studentID, StudentCreateDTO updatedStudentDto)
        {
            try
            {
                // Tìm student cần sửa theo StudentID
                var studentToUpdate = _context.student.FirstOrDefault(s => s.StudentID == studentID);

                if (studentToUpdate == null)
                {
                    return NotFound($"Student with ID '{studentID}' not found.");
                }

                // Cập nhật thông tin student từ DTO
                studentToUpdate.Name = updatedStudentDto.Name;
                studentToUpdate.Dob = updatedStudentDto.Dob;
                studentToUpdate.Sex = updatedStudentDto.Sex;
                studentToUpdate.Address = updatedStudentDto.Address;
                studentToUpdate.PhoneNumber = updatedStudentDto.PhoneNumber;
                studentToUpdate.EmailAddress = updatedStudentDto.EmailAddress;
                studentToUpdate.Country = updatedStudentDto.Country;
                studentToUpdate.ImageUrl = updatedStudentDto.ImageUrl;
                studentToUpdate.ModifiedDate = DateTime.Now;
                studentToUpdate.ModifiedUser = "API"; // User from API context

                // Lưu các thay đổi vào DbContext
                _context.SaveChanges();

                return Ok($"Student with ID '{studentID}' updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeleteStudent(string studentID)
        {
            try
            {
                // Tìm student cần xóa theo StudentID
                var studentToDelete = _context.student.FirstOrDefault(s => s.StudentID == studentID);

                if (studentToDelete == null)
                {
                    return NotFound($"Student with ID '{studentID}' not found.");
                }

                // Xóa student từ DbContext
                _context.student.Remove(studentToDelete);
                _context.SaveChanges();

                return Ok($"Student with ID '{studentID}' deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


    } 
    // DELETE: api/students/5
    
    
}
