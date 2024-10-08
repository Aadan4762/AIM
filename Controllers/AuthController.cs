using AIM.Data;
using AIM.Dtos;
using AIM.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AIM.Controllers;


 [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    { 
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Route For Seeding my roles to DB
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
             var seedRoles = await _authService.SeedRolesAsync();

            return Ok(seedRoles);
        }


        // Route -> Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterAsync(registerDto);

            if (registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }


        // Route -> Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);

            if(loginResult.IsSucceed)
                return Ok(loginResult);

            return Unauthorized(loginResult);
        }
        
        //generate access token
        [HttpPost("generate-another-access-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshDto tokenRefreshDto)
        {
            if (tokenRefreshDto == null || string.IsNullOrEmpty(tokenRefreshDto.RefreshToken))
            {
                return BadRequest(new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = "Invalid request"
                });
            }

            var response = await _authService.RefreshTokenAsync(tokenRefreshDto);

            if (!response.IsSucceed)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
        

        // Route -> make user -> admin
        [HttpPost]
        [Route("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
             var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

            if(operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make user -> owner
        [HttpPost]
        [Route("make-owner")]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeOwnerAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }
    }
