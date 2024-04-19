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
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<teacher>>> Getteacher()
        {
            return await _context.teacher.ToListAsync();
        }

        // GET: api/teachers/5
        [HttpGet]
        public async Task<ActionResult<teacher>> Getteacher(int id)
        {
            var teacher = await _context.teacher.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        [HttpPut(("Update"))]
        public IActionResult UpdateTeacher(int id, TeacherCreateDTO updatedTeacherDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tìm teacher cần sửa theo TeacherID
                    var teacherToUpdate = _context.teacher.FirstOrDefault(t => t.TeacherID == id);

                    if (teacherToUpdate == null)
                    {
                        return NotFound($"Teacher with ID '{id}' not found.");
                    }

                    // Cập nhật thông tin của teacher từ DTO
                    teacherToUpdate.Name = updatedTeacherDto.Name;
                    teacherToUpdate.Dob = updatedTeacherDto.Dob;
                    teacherToUpdate.Sex = updatedTeacherDto.Sex;
                    teacherToUpdate.PhoneNumber = updatedTeacherDto.PhoneNumber;
                    teacherToUpdate.EmailAddress = updatedTeacherDto.EmailAddress;
                    teacherToUpdate.Description = updatedTeacherDto.Description;
                    teacherToUpdate.ModifiedDate = DateTime.Now;
                    teacherToUpdate.ModifiedUser = "API"; // User from API context

                    // Lưu các thay đổi vào DbContext
                    _context.SaveChanges();

                    transaction.Commit(); // Lưu thay đổi vào database

                    return Ok($"Teacher with ID '{id}' updated successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Nếu có lỗi, rollback giao dịch
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }
        }


        // POST: api/teachers
        [HttpPost("Create")]
        public IActionResult CreateTeacher(TeacherCreateDTO teacherDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tạo một account mới
                    var account = new account
                    {
                        Name = teacherDto.EmailAddress, // Tạo Name ngẫu nhiên (có thể sử dụng UUID/GUID)
                        Password = teacherDto.Dob.ToString("MMyyyy"), // Password là MMyyyy của Dob
                        RoleID = 0, // Giả sử RoleID mặc định cho teacher là 2 (giáo viên)
                        Status = 1, // Giả sử Status mặc định là 1
                        CreatedDate = DateTime.Now,
                        CreatedUser = "API",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "API"
                    };

                    // Thêm account vào DbContext
                    _context.account.Add(account);
                    _context.SaveChanges(); // Lưu để có được ID của account sau khi được thêm vào

                    // Tạo một teacher và liên kết với account vừa tạo
                    var teacher = new teacher
                    {
                        Name = teacherDto.Name,
                        Dob = teacherDto.Dob,
                        Sex = teacherDto.Sex,
                        PhoneNumber = teacherDto.PhoneNumber,
                        EmailAddress = teacherDto.EmailAddress,
                        Description = teacherDto.Description,
                        AccountID = account.ID, // Sử dụng ID của account vừa được tạo
                        Status = 1,
                        CreatedDate = DateTime.Now,
                        CreatedUser = "API",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "API"
                    };

                    // Thêm teacher vào DbContext
                    _context.teacher.Add(teacher);
                    _context.SaveChanges(); // Lưu để hoàn thành việc thêm teacher

                    transaction.Commit(); // Lưu thay đổi vào database

                    return Ok("Teacher created successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Nếu có lỗi, rollback giao dịch
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteTeacher(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tìm teacher cần xóa theo TeacherID
                    var teacherToDelete = _context.teacher.FirstOrDefault(t => t.TeacherID == id);

                    if (teacherToDelete == null)
                    {
                        return NotFound($"Teacher with ID '{id}' not found.");
                    }

                    // Xóa teacher từ DbContext
                    _context.teacher.Remove(teacherToDelete);
                    _context.SaveChanges(); // Lưu thay đổi vào DbContext

                    // Xóa account tương ứng của teacher (nếu cần thiết)
                    var accountToDelete = _context.account.FirstOrDefault(a => a.ID == teacherToDelete.AccountID);
                    if (accountToDelete != null)
                    {
                        _context.account.Remove(accountToDelete);
                        _context.SaveChanges(); // Lưu thay đổi vào DbContext
                    }

                    transaction.Commit(); // Lưu thay đổi vào database

                    return Ok($"Teacher with ID '{id}' deleted successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Nếu có lỗi, rollback giao dịch
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }
        }

    }
}  
