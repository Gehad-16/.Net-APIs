using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{

    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager ,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [Authorize (AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var User = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email.Split('@')[0],
                PhoneNumber = registerDTO.PhoneNumber,
            };
            var Result =await _userManager.CreateAsync(User, registerDTO.Password);
            if(!Result.Succeeded)
                {
                    return BadRequest();
                }
            var ReturnedUser = new UserDTO()
            {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await _tokenService.CreateTokenAsync(User)
            };
            return Ok(ReturnedUser);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (User is null) return Unauthorized();

            var Result = await _signInManager.CheckPasswordSignInAsync(User, loginDTO.Password, false);

            if (!Result.Succeeded) return Unauthorized();

            var ReturnedUser = new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User)
            };
            return Ok(ReturnedUser);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
           var Email= User.FindFirstValue(ClaimTypes.Email);
            var user=await _userManager.FindByEmailAsync(Email);
            var ReturnObject = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user)
            };
            return Ok(ReturnObject);

        }
            
    }
}
