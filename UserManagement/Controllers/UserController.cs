using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public object Get()
        {
            var users = _context.Users;

            if (!users.Any())
                return NoContent();
            return Ok(users);
        }

        [HttpGet("{email}")]
        public object GetByEmail(string email)
        {
            return _context.Users.Where(b => b.Email == email).Select((c) => new
            {
                c.Name,
                c.Email,
                c.Salary,
                c.Expenses
            }).ToList();
        }
    }
}