using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AirDrop.Application.Interface;

namespace AirDrop.EndPoint.Controllers
{
    [Route("auth")]
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

        [HttpGet("telegram")]
        public async Task<IActionResult> TelegramAuth([FromQuery] string hash, [FromQuery] string auth_date, [FromQuery] string id, [FromQuery] string first_name, [FromQuery] string last_name, [FromQuery] string username, [FromQuery] string photo_url)
        {
            var dataCheckString = $"auth_date={auth_date}\nid={id}\nusername={username}";

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_botToken)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
                var calculatedHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                if (calculatedHash != hash)
                    return Unauthorized("Invalid Telegram login");
            }

            var user = await _userService.GetOrCreateUserAsync(
            long.Parse(id), username, first_name, last_name, photo_url);

            var token = GenerateJwtToken(user);
            return Ok(new { token });
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
