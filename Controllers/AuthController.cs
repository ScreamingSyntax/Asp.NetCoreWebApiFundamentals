using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test.Models.DTO;
using Test.Repositories;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.EmailAddress,
                Email = registerRequestDto.EmailAddress,

            };
            IdentityResult result = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (result.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    result = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles!);

                    if (result.Succeeded)
                    {
                       
                        return Ok("User Was Registered, Please Login");
                    }

                    // Add Roles to this user
                }
            }
            return BadRequest("Something wen't wrong");
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
                bool checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null && roles.Any())
                    {
                      var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(jwtToken);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
