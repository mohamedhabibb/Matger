using System.Security.Claims;
using Matger.Core.Entity.Identity;
using Matger.Core.Services;
using Matger.DTOs;
using Matger.Errors;
using Matger.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Matger.Controllers
{
    
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenSevice _tokenSevice;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager , ITokenSevice tokenSevice)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenSevice = tokenSevice;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "Email is aready in use"));
            }

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            var Result = await _userManager.CreateAsync(user, model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));

            var ReturnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenSevice.GetTokenSeviceAsync(user , _userManager)
            };

            return ReturnedUser;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));

            var Results = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!Results.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenSevice.GetTokenSeviceAsync(user, _userManager)

            });
        }


        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email); //User exist inside controller 
            var user = await _userManager.FindByEmailAsync(email);
            var ReturnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenSevice.GetTokenSeviceAsync(user, _userManager),
            };
            return Ok(ReturnedUser);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindUserWithAddress(User); //this function from UserManagerExtension class

            var ReturnedAddress = new AddressDto()
            {
                Id=user.Address.Id, 
                FirstName = user.Address.FirstName,
                LastName = user.Address.LastName,
                City = user.Address.City,
                Street = user.Address.Street,
                Country = user.Address.Country,
            };

            return Ok(ReturnedAddress);
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<UserDto>> UpdateAddress(AddressDto udatedAddress)
        {
            var user = await _userManager.FindUserWithAddress(User); //this function from UserManagerExtension class
            udatedAddress.Id = user.Address.Id;
            user.Address.FirstName = udatedAddress.FirstName;
            user.Address.LastName = udatedAddress.LastName;
            user.Address.City = user.Address.City;
            user.Address.Street = user.Address.Street;
            user.Address.Country = user.Address.Country;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(udatedAddress);

            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
