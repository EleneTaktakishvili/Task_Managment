using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagment.API.Models;
using TaskManagment.Application.Services;
using TaskManagment.Domain.Entities;

namespace TaskManagment.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(UserManager<User> userManager, JwtTokenService jwtTokenService, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login method.");
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogInformation("Unauthorized Login.");
                return Unauthorized();
            }

            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(user);

            return Ok(new RefreshTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            var user = new Domain.Entities.User { }; // Here we need method to get user by refreshtoken from database
                                                     // await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _logger.LogInformation("Unauthorized Login.");
                return Unauthorized();
            }


            var claims = new[]
         {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var newAccessToken = _jwtTokenService.GenerateAccessToken(claims);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(user);

            return Ok(new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken

            });
        }
    }
}
