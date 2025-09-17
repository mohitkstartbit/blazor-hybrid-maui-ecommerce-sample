
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VeggieApp.Model.Model.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VeggieApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public LogInController(IConfiguration configuration, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LogInModel login)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(login.Email!, login.Password!, false, false);

        //    if (!result.Succeeded) return BadRequest(new LogInResult { Successful = false, Error = "Username and password are not match " });
        //    var user = await _userManager.GetUserAsync(User);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name,login.Email!),
        //        new Claim(ClaimTypes.NameIdentifier,user.Id)
        //    };

        //    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
        //    var creads = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        //    var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

        //    var token = new JwtSecurityToken(
        //            _configuration["JwtIssuer"],
        //            _configuration["JwtAudience"],
        //            claims,
        //            expires: expiry,
        //            signingCredentials: creads);

        //    return Ok(new LogInResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        //}
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LogInModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email!)
            };

            // Add roles as claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

    }
}
