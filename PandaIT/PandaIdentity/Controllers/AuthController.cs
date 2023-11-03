using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using PandaIdentity.Dto;
using PandaIdentity.Interfaces;
using PandaIdentity.Repository;

namespace PandaIdentity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        //private readonly IUserRepository userRepository;
        //private readonly ITokenHandler tokenHandler;

        //public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        //{
        //    this.userRepository = userRepository;
        //    this.tokenHandler = tokenHandler;
        //}
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) 
        {
            var isExist = await _userManager.FindByEmailAsync(registerRequestDto.UserName);
            if (isExist == null) {
                IdentityUser identityUser=new IdentityUser();
                identityUser.UserName= registerRequestDto.UserName;
                identityUser.Email = registerRequestDto.UserName;
            
                var identityResult=await _userManager.CreateAsync(identityUser,registerRequestDto.Password);
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
            // Check if exist
            var userExist = await _userManager.FindByEmailAsync(logInRequestDto.UserName);

            // Check if user is authenticated
            if (userExist!=null)
            {
                var checkPass= await _userManager.CheckPasswordAsync(userExist,logInRequestDto.Password);
                if (checkPass)
                {
                    //Get Roles for the user
                    var roles = await _userManager.GetRolesAsync(userExist);
                    if (roles!=null)
                    {
                        var jwtToken=_tokenRepository.CreateJWTToken(userExist,roles.ToList());
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
