using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Models;
using BCrypt.Net;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // 🔥 CREATE USER (FIRST USER WITHOUT AUTH, OTHERS REQUIRE ADMIN)
        [HttpPost]
        public IActionResult CreateUser([FromBody] User request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.PasswordHash))
                return BadRequest("Username and Password required");

            if (_context.Users.Any(u => u.Username == request.Username))
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User created successfully");
        }

        // 🔍 GET ALL USERS (ADMIN + ACCOUNTANT)
        [Authorize(Roles = "Admin,Accountant")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Username
                })
                .ToList();

            return Ok(users);
        }

        // 👑 ADD ROLE (ONLY ADMIN)
        [Authorize(Roles = "Admin")]
        [HttpPost("add-role")]
        public IActionResult AddRole([FromBody] string roleName)
        {
            if (_context.Roles.Any(r => r.Name == roleName))
                return BadRequest("Role already exists");

            var role = new Role
            {
                Name = roleName
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return Ok("Role added successfully");
        }

        // 🔗 ASSIGN ROLE TO USER (ONLY ADMIN)
        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public IActionResult AssignRole(int userId, int roleId)
        {
            var user = _context.Users.Find(userId);
            var role = _context.Roles.Find(roleId);

            if (user == null || role == null)
                return NotFound("User or Role not found");

            var exists = _context.UserRoles
                .Any(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (exists)
                return BadRequest("Role already assigned");

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            return Ok("Role assigned successfully");
        }
    }
}