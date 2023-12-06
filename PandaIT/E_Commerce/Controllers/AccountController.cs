using E_Commerce.Interface.Auth;
using E_Commerce.Models.Dto.Request;
using E_Commerce.Models.Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var isExist = await _userManager.FindByEmailAsync(registerRequestDto.UserName);
            if (isExist == null)
            {
                IdentityUser identityUser = new IdentityUser();
                identityUser.UserName = registerRequestDto.UserName;
                identityUser.Email = registerRequestDto.UserName;

                var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
                if (identityResult.Succeeded)
                {
                    if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                    {
                        identityResult = await _userManager.AddToRoleAsync(identityUser, registerRequestDto.Roles);
                        if (identityResult.Succeeded)
                        {
                            return Ok($"{registerRequestDto.UserName} Created Successfully");
                        }
                    }
                }
            }
            return BadRequest("Something went wrong");
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LogInRequestDto logInRequestDto)
        {
            // Check if exist by user name or email
            var userExist = await _userManager.FindByEmailAsync(logInRequestDto.UserName);

            // Check if user is authenticated
            if (userExist != null)
            {
                // Verify by password
                var checkPass = await _userManager.CheckPasswordAsync(userExist, logInRequestDto.Password);
                if (checkPass)
                {
                    //Get Roles for the user
                    var roles = await _userManager.GetRolesAsync(userExist);
                    if (roles != null)
                    {
                        //Passing user email and roles to create a token
                        var jwtToken = _tokenService.CreateJWTToken(userExist, roles.ToList());
                        LogInResponseDto logInResponseDto = new LogInResponseDto();
                        logInResponseDto.JwtToken = jwtToken;
                        return Ok(logInResponseDto);
                    }
                }
            }

            return BadRequest("Username or Password is incorrect.");
        }

    }
}
