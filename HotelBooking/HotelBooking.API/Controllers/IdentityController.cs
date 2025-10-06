using HotelBooking.Application.Identity.Authentication.AuthContracts;
using HotelBooking.Application.Identity.Authentication.Interfaces;
using HotelBooking.Domain.Contracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO userRegisterDto)
        {
            try
            {
                await _identityService.Register(userRegisterDto);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO userLoginDto)
        {
            var token = await _identityService.Login(userLoginDto);

            Response.Cookies.Append("refreshToken", token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
            });

            Response.Cookies.Append("accessToken", token.AccessToken, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                return Unauthorized();

            var newToken = await _identityService.RefreshToken(refreshToken);

            Response.Cookies.Append("refreshToken", newToken.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
            });

            Response.Cookies.Append("accessToken", newToken.AccessToken, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            return Ok(newToken);
        }
    }
}
