using System;
using System.Linq;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Models.Validators;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public object GetByEmail(string email)
        {
            var user = new User {Email = email};
            var validator = new UserValidator();
            
            try
            {
                validator.TestValidate(user).ShouldNotHaveValidationErrorFor(x => x.Email);
            }
            catch (ValidationTestException exception)
            {
                return BadRequest("Email format is not acceptable");
            }
            catch (Exception exception)
            {
                return StatusCode(500);
            }

            return _context.Users.Where(b => b.Email == user.Email).ToList();
        }
    }
}