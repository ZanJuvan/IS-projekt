using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeeOrganizer.Data;
using BeeOrganizer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace BeeOrganizer.Controllers_Api
{
    [Route("api/login")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly Cebelarstvo _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginApiController> _logger;
        private readonly IConfiguration _config;

        public LoginApiController(Cebelarstvo context, SignInManager<ApplicationUser> signInManager, ILogger<LoginApiController> logger, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
            _config = config;
            _userManager = userManager;
        }

        [BindProperty]
        
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        
        [TempData]
        public string ErrorMessage { get; set; }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Validate the username and password using your chosen authentication scheme
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }

        // If the authentication is successful, generate an API key
        var apiKey = GenerateApiKey();

        // Return the API key to the client
        return Ok(new
        {
            ApiKey = apiKey,
            Id = user.Id,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName
        });
        }

        private string GenerateApiKey()
        {
            // Generate a new API key using a secure random number generator
            // You can use any method you like to generate the API key
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

    }
}