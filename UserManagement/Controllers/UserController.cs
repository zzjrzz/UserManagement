using System;
using System.Linq;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Models;
using UserManagement.Models.Validators;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ApiDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogError($"{exception.Message} : {exception.StackTrace}");
                return BadRequest("Email format is not acceptable");
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message} : {exception.StackTrace}");
                return StatusCode(500);
            }

            return _context.Users.Where(b => b.Email == user.Email).ToList();
        }
    }
}