using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
       
        public async Task<IActionResult> Login([FromBody]clsLoginParam request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var account = await _context.account
                    .SingleOrDefaultAsync(a => a.Name == request.Name && a.Password == request.Password);

                if (account == null)
                {
                    return NotFound("Invalid username or password");
                }

                if (account.RoleID == 0) // Nếu là tài khoản giáo viên
                {
                    var teacher = await _context.teacher.FirstOrDefaultAsync(t => t.AccountID == account.ID);
                    if (teacher == null)
                    {
                        return NotFound("Teacher profile not found");
                    }
                    return Ok("Login successful as a teacher");
                }
                else if (account.RoleID == 1) // Nếu là tài khoản sinh viên
                {
                    var student = await _context.student.FirstOrDefaultAsync(s => s.AccountID == account.ID);
                    if (student == null)
                    {
                        return NotFound("Student profile not found");
                    }
                    return Ok("Login successful as a student");
                }
                else // Nếu RoleID không hợp lệ
                {
                    return BadRequest("Invalid RoleID");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

         
        }

        [HttpGet]
        public async Task<IActionResult> GetALlAccount()
        {
            return Ok("OK");
        }
    }
}
