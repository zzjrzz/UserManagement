using System;
using System.Linq;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IValidator<User> _validator;

        public UserController(
            ApiDbContext context,
            ILogger<UserController> logger,
            IValidator<User> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
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

            try
            {
                _validator.TestValidate(user).ShouldNotHaveValidationErrorFor(x => x.Email);
            }
            catch (ValidationTestException exception)
            {
                _logger.LogWarning($"{exception.Message} : {exception.StackTrace}");
                return BadRequest("Email format is not acceptable");
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message} : {exception.StackTrace}");
                return StatusCode(500);
            }

            return _context.Users.Where(b => b.Email == user.Email).ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public object Create([FromBody] User user)
        {
            try
            {
                if (!_validator.TestValidate(user).IsValid)
                {
                    var errorReturned = _validator.TestValidate(user).Errors.Aggregate("", 
                        (current, error) => current +  error.ErrorMessage);
                    return BadRequest(errorReturned);
                }

                if (_context.Users.Any(existingUser => existingUser.Email == user.Email))
                {
                    return Conflict("user email already exists");
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return Created("/api/create", user);
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message} : {exception.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}