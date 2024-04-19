using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using QLDoAn.Models;
using quanlidoan.Data;

namespace QLDoAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountsController : ControllerBase
    {
        private readonly DataContext _context;

        public accountsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Lấy thông tin người dùng đã xác thực từ HttpContext
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //if (userId == null)
                //{
                //    return Unauthorized(); // Người dùng chưa đăng nhập
                //}

                // Tìm tài khoản dựa trên ID của người dùng đã xác thực
                var account = await _context.account.Where(x=>x.ID == model.UserID && x.Password == model.CurrentPassword).FirstOrDefaultAsync();

                if (account == null)
                {
                    return NotFound("Account not found");
                }
                else
                {
                    account.Password = model.NewPassword;
                    account.ModifiedDate = DateTime.Now;
                    account.Status = 2;
                }
                
                // Lưu các thay đổi vào cơ sở dữ liệu
                //_context.account.Update(account);
                await _context.SaveChangesAsync();

                return Ok("Password changed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/accounts
        [HttpPost("Register")]
        public async Task<IActionResult> Register(string Name, string Password, int RoleID, string StudentID, string StudentName, DateTime Dob, int Sex, string Address, string PhoneNumber, string EmailAddress, string Country, string ImageUrl, string Description = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Tạo một tài khoản mới
                var account = new account
                {
                    Name = Name,
                    Password = Password,
                    RoleID = RoleID,
                    Status = 1, 
                    CreatedDate = DateTime.Now,
                    CreatedUser = "System", 
                    ModifiedDate = DateTime.Now,
                    ModifiedUser = "System"
                };

                _context.account.Add(account);
                await _context.SaveChangesAsync();

                if (account.RoleID == 1) // Nếu đăng ký là sinh viên
                {
                    var student = new student
                    {
                        StudentID = StudentID,
                        Name = StudentName,
                        Dob = Dob,
                        Sex = Sex,
                        Address = Address,
                        PhoneNumber = PhoneNumber,
                        EmailAddress = EmailAddress,
                        Country = Country,
                        ImageUrl = ImageUrl,
                        Status = 1, // Trạng thái mặc định (hoạt động)
                        CreatedDate = DateTime.Now,
                        CreatedUser = "System",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "System",
                        AccountID = account.ID
                    };

                    _context.student.Add(student);
                }
                else if (account.RoleID == 0) // Nếu đăng ký là giáo viên
                {
                    var teacher = new teacher
                    {
                        Name = Name,
                        Dob = Dob,
                        Sex = Sex,
                        PhoneNumber = PhoneNumber,
                        EmailAddress = EmailAddress,
                        Description = Description,
                        Status = 1, // Trạng thái mặc định (hoạt động)
                        CreatedDate = DateTime.Now,
                        CreatedUser = "System",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "System",
                        AccountID = account.ID
                    };

                    _context.teacher.Add(teacher);
                }

                await _context.SaveChangesAsync();

                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] clsLoginParam request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Find the account based on username and password
                var account = await _context.account.SingleOrDefaultAsync(account => account.Name == request.Name && account.Password == request.Password);

                if (account == null)
                {
                    return NotFound("Invalid username or password");
                }

                // Determine the role of the account
                if (account.RoleID == 0) // Teacher
                {
                    // Load teacher information
                    var teacher = await _context.teacher.FirstOrDefaultAsync(t => t.AccountID == account.ID);
                    if (teacher == null)
                    {
                        return NotFound("Teacher profile not found");
                    }

                    // Return teacher information along with account details
                    return Ok(new { Account = account, TeacherInfo = teacher });
                }
                else if (account.RoleID == 1) // Student
                {
                    // Load student information
                    var student = await _context.student.FirstOrDefaultAsync(s => s.AccountID == account.ID);
                    if (student == null)
                    {
                        return NotFound("Student profile not found");
                    }

                    // Return student information along with account details
                    return Ok(new { Account = account, StudentInfo = student });
                }
                else
                {
                    // If the RoleID is neither 0 (Teacher) nor 1 (Student)
                    return BadRequest("Invalid RoleID");
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetALlAccount()
        {
            return Ok("OK");
        }
    }
}
