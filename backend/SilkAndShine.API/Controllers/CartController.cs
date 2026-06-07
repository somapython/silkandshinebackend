using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.Models;
using System.Security.Claims;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly AppDbContext _context;

    public CartController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddToCart(
        int productId
    )
    {
        var userId =
        int.Parse(
            User.FindFirst(
                ClaimTypes.NameIdentifier
            )!.Value
        );

        var existingItem =
            _context.Carts.FirstOrDefault(
                x =>
                    x.UserId == userId &&
                    x.ProductId == productId
            );

        if (existingItem != null)
        {
            existingItem.Quantity++;

            _context.SaveChanges();

            return Ok(
                "Quantity Updated"
            );
        }

        var cart = new Cart
        {
            UserId = userId,

            ProductId = productId,

            Quantity = 1
        };

        _context.Carts.Add(cart);

        _context.SaveChanges();

        return Ok(
            "Added To Cart"
        );
    }

    [HttpGet]
public IActionResult GetCart()
{
    var userId =
    int.Parse(
        User.FindFirst(
            ClaimTypes.NameIdentifier
        )!.Value
    );

    var cart =
        _context.Carts
        .Where(x => x.UserId == userId)
        .ToList();

    return Ok(cart);
}
}