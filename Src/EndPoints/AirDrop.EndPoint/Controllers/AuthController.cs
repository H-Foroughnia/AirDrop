using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirDrop.Application.Interface;
using AirDrop.Domain.DTO;

namespace AirDrop.EndPoint.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _jwtSecretKey = "8f71648e-6a50-4cde-8474-bb02a9463b1c";
        private readonly TimeSpan _tokenLifetime = TimeSpan.FromHours(1);
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("telegram")]
        public async Task<IActionResult> TelegramAuth([FromBody] TelegramAuthRequestDto request)
        {
            if (request == null)
            {
                _logger.LogWarning("Received a null TelegramAuthRequestDto.");
                return BadRequest("Invalid request");
            }

            _logger.LogInformation("Processing TelegramAuthRequestDto with the following details: " +
                                   "TelegramId: {Id}, FirstName: {FirstName}, LastName: {LastName}, Username: {Username}",
                request.Id, request.FirstName, request.LastName, request.Username);

            try
            {
                var user = await _userService.AuthenticateOrRegisterTelegramUserAsync(
                    request.Id,
                    request.FirstName,
                    request.LastName,
                    request.Username
                );
                return Ok(user);

                //var jwtToken = GenerateJwtToken(user);

                //_logger.LogInformation("User authenticated successfully. Token generated for UserId: {UserId}", user.Id);

                //return Ok(new { Token = jwtToken, User = user });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing TelegramAuth.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
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
