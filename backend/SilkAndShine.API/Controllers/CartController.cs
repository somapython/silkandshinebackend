// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using SilkAndShineAPI.Data;
// using SilkAndShineAPI.Models;
// using System.Security.Claims;

// namespace SilkAndShineAPI.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// [Authorize]
// public class CartController : ControllerBase
// {
//     private readonly AppDbContext _context;

//     public CartController(
//         AppDbContext context
//     )
//     {
//         _context = context;
//     }
//     public class AddCartDto
//     {
//         public int ProductId { get; set; }
//     }
    
//     [HttpPost]
//     public IActionResult AddToCart(
//         int productId
//     )
//     {
//         var userId =
//         int.Parse(
//             User.FindFirst(
//                 ClaimTypes.NameIdentifier
//             )!.Value
//         );

//         var existingItem =
//             _context.Carts.FirstOrDefault(
//                 x =>
//                     x.UserId == userId &&
//                     x.ProductId == productId
//             );

//         if (existingItem != null)
//         {
//             existingItem.Quantity++;

//             _context.SaveChanges();

//             return Ok(
//                 "Quantity Updated"
//             );
//         }

//         var cart = new Cart
//         {
//             UserId = userId,

//             ProductId = productId,

//             Quantity = 1
//         };

//         _context.Carts.Add(cart);

//         _context.SaveChanges();

//         return Ok(
//             "Added To Cart"
//         );
//     }

//     [HttpGet]
// public IActionResult GetCart()
// {
//     var userId =
//     int.Parse(
//         User.FindFirst(
//             ClaimTypes.NameIdentifier
//         )!.Value
//     );

//     var cart =
//         _context.Carts
//         .Where(x => x.UserId == userId)
//         .ToList();

//     return Ok(cart);
// }
// }

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

    public class AddCartDto
    {
        public int ProductId { get; set; }
    }

    [HttpPost]
public IActionResult AddToCart(
    [FromBody] AddCartDto dto
)
{
    try
    {
        var claim =
            User.FindFirst(
                ClaimTypes.NameIdentifier
            );

        if(claim == null)
        {
            return BadRequest(
                "User Claim Missing"
            );
        }

        var userId =
            int.Parse(
                claim.Value
            );

        var productId =
            dto.ProductId;

        Console.WriteLine(
            $"UserId={userId}"
        );

        Console.WriteLine(
            $"ProductId={productId}"
        );

        var existingItem =
            _context.Carts.FirstOrDefault(
                x =>
                    x.UserId == userId &&
                    x.ProductId == productId
            );

        if(existingItem != null)
        {
            existingItem.Quantity++;

            _context.SaveChanges();

            return Ok(new
            {
                message = "Quantity Updated"
            });
        }

        var cart =
            new Cart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1,
                CreatedAt = DateTime.Now
            };

        _context.Carts.Add(cart);

        _context.SaveChanges();

        return Ok(new
        {
            message = "Added To Cart"
        });
    }
    catch(Exception ex)
    {
        return BadRequest(new
        {
            error = ex.Message,
            stackTrace = ex.StackTrace
        });
    }
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
        .Join(
            _context.Products,
            c => c.ProductId,
            p => p.Id,
            (c,p) => new
            {
                c.Id,
                c.Quantity,

                Product = new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ImageUrl,
                    p.Category
                }
            }
        )
        .ToList();

    return Ok(cart);
}

    [HttpPut("{id}")]
    public IActionResult UpdateQuantity(
        int id,
        int quantity
    )
    {
        var cartItem =
            _context.Carts.FirstOrDefault(
                x => x.Id == id
            );

        if (cartItem == null)
        {
            return NotFound();
        }

        cartItem.Quantity = quantity;

        _context.SaveChanges();

        return Ok(new
        {
            message = "Cart Updated"
        });
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveFromCart(
        int id
    )
    {
        var cartItem =
            _context.Carts.FirstOrDefault(
                x => x.Id == id
            );

        if (cartItem == null)
        {
            return NotFound();
        }

        _context.Carts.Remove(
            cartItem
        );

        _context.SaveChanges();

        return Ok(new
        {
            message = "Item Removed"
        });
    }

}