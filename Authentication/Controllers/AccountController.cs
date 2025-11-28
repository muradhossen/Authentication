using Application.DTOs;
using Application.Services.Accounts;
using Authentication.Controllers.Base;
using Domain.Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers;

public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    private readonly IAccountService _accountService;

    public AccountController(ITokenService tokenService
      , UserManager<User> userManager
      , SignInManager<User> signInManager

      , IAccountService accountService)
    {

        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;

        _accountService = accountService;
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDTO registerDto)
    {
        if (await _accountService.IsUserExist(registerDto.Username)) return BadRequest("User already exist.");

        if (string.IsNullOrEmpty(registerDto.Password) || string.IsNullOrEmpty(registerDto.Username))
            return BadRequest("User name or Password can't be empty");

        var user = new User
        {
            Address = registerDto.Address,
            UserName = registerDto.Username.ToLower(),
            Email = registerDto.Email,
            DateOfBirth = registerDto.DateOfBirth,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Gender = registerDto.Gender,
            PhoneNumber = registerDto.PhoneNumber,
            PhotoUrl = registerDto.PhotoUrl,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

        if (!roleResult.Succeeded)
        {
            return BadRequest(roleResult.Errors);
        }

        var userDto = new UserDTO
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber,
            PhotoUrl = user.PhotoUrl,
            DateOfBirth = user.DateOfBirth,
            Address = user.Address,
            Email = user.Email,
            Token = await _tokenService.CreateToken(user)
        };

        return Ok(userDto);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var user = await _accountService.GetUserByUsername(loginDto.UserName);

        if (user == null) return Unauthorized("Invalid username");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        var userDto = new UserDTO
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber,
            PhotoUrl = user.PhotoUrl,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Token = await _tokenService.CreateToken(user)
        };

        return Ok(userDto);
    }
}
