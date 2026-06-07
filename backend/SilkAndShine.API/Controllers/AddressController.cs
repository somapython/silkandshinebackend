
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.Models;
using System.Security.Claims;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AddressController : ControllerBase
{
    private readonly AppDbContext _context;

    public AddressController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddAddress(
        Address address
    )
    {
        var userId =
        int.Parse(
            User.FindFirst(
                ClaimTypes.NameIdentifier
            )!.Value
        );

        address.UserId = userId;

        _context.Addresses.Add(
            address
        );

        _context.SaveChanges();

        return Ok();
    }

    [HttpGet]
    public IActionResult GetAddresses()
    {
        var userId =
        int.Parse(
            User.FindFirst(
                ClaimTypes.NameIdentifier
            )!.Value
        );

        return Ok(
            _context.Addresses
            .Where(
                x => x.UserId == userId
            )
            .ToList()
        );
    }
}