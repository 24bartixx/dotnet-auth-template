using AuthApiTemplate.Dtos.Auth;
using AuthApiTemplate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthApiTemplate.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                
            AppUser newAppUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            IdentityResult? userCreationResult = await _userManager.CreateAsync(newAppUser, registerDto.Password);

            if (userCreationResult.Succeeded)
            {
                IdentityResult roleAdditionResult = await _userManager.AddToRoleAsync(newAppUser, "User");
                if (roleAdditionResult.Succeeded)
                {
                    // TODO: generate token and pass it to created
                    return Created();
                }
                else
                {
                    return StatusCode(500, roleAdditionResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, userCreationResult.Errors);
            }

            return NotFound();
        }
    }
}