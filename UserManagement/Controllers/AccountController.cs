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
    public class AccountController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IValidator<Account> _validator;

        public AccountController(
            ApiDbContext context,
            ILogger<AccountController> logger,
            IValidator<Account> validator)
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
            var accounts = _context.Accounts;

            if (!accounts.Any())
                return NoContent();
            return Ok(accounts.Select((a) => new
            {
                a.User.Name,
                a.User.Email,
                a.User.Expenses,
                a.User.Salary
            }).ToList());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public object Create(string email)
        {
            try
            {
                if (!_context.Users.Any(u => u.Email == email))
                    return UnprocessableEntity("user for creating account cannot be found");
                {
                    var account = new Account {User = _context.Users.First(u => u.Email == email)};
                    if (!_validator.TestValidate(account).IsValid)
                    {
                        return UnprocessableEntity("user is not eligible for account with salary - expenses < 1000");
                    }

                    _context.Accounts.Add(account);
                    _context.SaveChanges();
                    return Created("/api/account", account);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message} : {exception.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}