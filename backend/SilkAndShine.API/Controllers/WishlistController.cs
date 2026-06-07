
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.Models;
using System.Security.Claims;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController
    : ControllerBase
{
    private readonly AppDbContext _context;

    public WishlistController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddToWishlist(
        int productId
    )
    {
        var userId =
        int.Parse(
            User.FindFirst(
                ClaimTypes.NameIdentifier
            )!.Value
        );

        var item =
        new Wishlist
        {
            UserId = userId,
            ProductId = productId
        };

        _context.Wishlists.Add(item);

        _context.SaveChanges();

        return Ok(
            "Added to Wishlist"
        );
    }
}