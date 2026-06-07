
using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(
            new
            {
                TotalProducts =
                _context.Products.Count(),

                TotalUsers =
                _context.Users.Count(),

                TotalWishlist =
                _context.Wishlists.Count(),

                TotalCartItems =
                _context.Carts.Count()
            }
        );
    }
}