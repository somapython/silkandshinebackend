using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(
            _context.Users.ToList()
        );
    }

    [HttpPut("{id}/role")]
    public IActionResult UpdateRole(
        int id,
        [FromQuery] string role
    )
    {
        var user =
            _context.Users
            .FirstOrDefault(
                x => x.Id == id
            );

        if(user == null)
        {
            return NotFound();
        }

        user.Role = role;

        _context.SaveChanges();

        return Ok(
            "Role Updated"
        );
    }
}