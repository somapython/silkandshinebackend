using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/admin/orders")]
public class AdminOrderController
    : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminOrderController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllOrders()
    {
        return Ok(
            _context.Orders
            .OrderByDescending(
                x => x.CreatedAt
            )
            .ToList()
        );
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStatus(
        int id,
        string status
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

        order.Status = status;

        _context.SaveChanges();

        return Ok(
            "Order Updated"
        );
    }
}