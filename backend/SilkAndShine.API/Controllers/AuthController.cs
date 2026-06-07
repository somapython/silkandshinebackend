using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.DTOs;
using SilkAndShineAPI.Models;
using SilkAndShineAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(
        AppDbContext context,
        IConfiguration config
    )
    {
        _context = context;
        _config = config;
    }

    [HttpPost("register")]
    public IActionResult Register(
        RegisterDto dto
    )
    {
        var userExists =
            _context.Users.Any(
                x => x.Mobile == dto.Mobile
            );

        if (userExists)
        {
            return BadRequest(
                "Mobile already exists"
            );
        }

        var user = new User
        {
            FullName = dto.FullName,

            Mobile = dto.Mobile,

            Email = dto.Email,

            PinHash =
                BCrypt.Net.BCrypt.HashPassword(
                    dto.Pin
                ),

            PasswordHash =
                string.IsNullOrEmpty(
                    dto.Password
                )
                ? null
                : BCrypt.Net.BCrypt.HashPassword(
                    dto.Password
                ),

            CreatedAt = DateTime.Now
        };

        _context.Users.Add(user);

        _context.SaveChanges();

        return Ok(
            new
            {
                message =
                "User Registered Successfully"
            }
        );
    }

    [HttpPost("login")]
    public IActionResult Login(
        LoginDto dto
    )
    {
        var user =
            _context.Users.FirstOrDefault(
                x => x.Mobile == dto.Mobile
            );

        if (user == null)
        {
            return Unauthorized(
                "User not found"
            );
        }

        bool validPin =
            BCrypt.Net.BCrypt.Verify(
                dto.Pin,
                user.PinHash
            );

        if (!validPin)
        {
            return Unauthorized(
                "Invalid PIN"
            );
        }

        var token =
            JwtHelper.GenerateToken(
                _config,
                user.Id,
                user.Mobile
            );

        return Ok(
            new
            {
                token,

                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Mobile,
                    user.Email,
                    user.Role
                }
            }
        );
    }
    [Authorize]
[HttpGet("profile")]
public IActionResult Profile()
{
    var userId =
        User.FindFirst(
            ClaimTypes.NameIdentifier
        )?.Value;

    var mobile =
        User.FindFirst(
            ClaimTypes.MobilePhone
        )?.Value;

    return Ok(new
    {
        UserId = userId,
        Mobile = mobile
    });
}
}