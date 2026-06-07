using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.Models;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(
            _context.Orders
            .OrderByDescending(
                x => x.CreatedAt
            )
            .ToList()
        );
    }

    [HttpPost]
    public IActionResult CreateOrder(
        Order order
    )
    {
        order.Status =
            "Pending";

        order.PaymentStatus =
            "Pending";

        order.CreatedAt =
            DateTime.Now;

        _context.Orders.Add(
            order
        );

        _context.SaveChanges();

        return Ok(order);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder(
        int id
    )
    {
        var order =
            _context.Orders
            .FirstOrDefault(
                x => x.Id == id
            );

        if(order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost("place")]
public IActionResult PlaceOrder(
    Order order
)
{
    order.Status =
        "Pending";

    order.PaymentStatus =
        "Pending";

    order.CreatedAt =
        DateTime.Now;

    _context.Orders.Add(
        order
    );

    _context.SaveChanges();

    return Ok(
        new
        {
            message =
            "Order Placed",

            order.Id
        }
    );
}
}