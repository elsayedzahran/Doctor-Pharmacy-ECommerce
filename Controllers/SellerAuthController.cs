using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Dtos;
using ProjectTest.Models;
using ProjectTest.Services;
using System.Security.Claims;

namespace ProjectTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerAuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;

        public SellerAuthController(ITokenService tokenService,
            UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] SellerForRegisterDto userForRegisterDto)
        {
            //check if email already exist
            var email_exist = await _userManager.FindByEmailAsync(userForRegisterDto.Email);
            if (email_exist != null)
                return BadRequest("email already exist");

            //create the user
            var user = new UserEntity
            {
                Email = userForRegisterDto.Email,
                UserName = userForRegisterDto.Name,
                CityId = userForRegisterDto.CityId,
            };

            var is_Created = await _userManager.CreateAsync(user, userForRegisterDto.Password);
            var roles_added = await _userManager.AddToRoleAsync(user, "Seller");

            if (!is_Created.Succeeded || !roles_added.Succeeded)
                return BadRequest("Server Error");

            var token = await _tokenService.CreateToken(user);
            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);

            if (user is null)
                return Unauthorized("Email or password in correct.");

            var role = await _userManager.GetRolesAsync(user);
            if (role.Count == 1 && role[0] != "Seller")
                return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, userForLoginDto.Password);

            if (!result)
            {
                return Unauthorized("Email or password in correct.");
            }
            var token = await _tokenService.CreateToken(user);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordDto request)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId == Guid.Empty)
                return BadRequest("can't find user Id");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return BadRequest($"can't find user with the id = {userId.ToString()}");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
                return BadRequest("Server Error");

            return Ok("Changed");
        }

    }
}
