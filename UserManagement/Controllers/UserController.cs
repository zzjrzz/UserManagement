using System.Linq;
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
        public object Get()
        {
            return _context.Users.Select((c) => new
            {
                c.Name,
                c.Email,
                c.Salary,
                c.Expenses
            }).ToList();
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