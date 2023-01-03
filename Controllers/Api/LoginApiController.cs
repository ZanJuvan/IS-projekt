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

        public LoginApiController(SignInManager<ApplicationUser> signInManager, ILogger<LoginApiController> logger, IConfiguration config)
        {
            _signInManager = signInManager;
            _logger = logger;
            _config = config;
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

        // If the authentication is successful, generate a JWT containing the user's claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id)
        };
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Return the JWT to the client
        return Ok(new
        {
            Token = tokenString
        });
    }


    }
}