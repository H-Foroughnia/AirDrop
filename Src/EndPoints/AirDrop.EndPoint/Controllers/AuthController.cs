using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirDrop.Application.Interface;

namespace AirDrop.EndPoint.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _botToken = "botToken";
        private readonly string _jwtSecretKey = "8f71648e-6a50-4cde-8474-bb02a9463b1c";
        private readonly TimeSpan _tokenLifetime = TimeSpan.FromHours(1);
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("telegram")]
        public async Task<IActionResult> TelegramAuth([FromBody] string id, [FromBody] string first_name, [FromBody] string last_name, [FromBody] string username)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid request");

            var user = await _userService.AuthenticateOrRegisterTelegramUserAsync(id, first_name, last_name, username);

            if (user == null)
                return BadRequest("User registration failed");

            var jwtToken = GenerateJwtToken(user);
            return Ok(new { Token = jwtToken, User = user });
        }

        private string GenerateJwtToken(dynamic user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.Username ?? ""),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? ""),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: "ourAirDrop",
                audience: "ourAirDrop",
                claims: claims,
                expires: DateTime.UtcNow.Add(_tokenLifetime),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
