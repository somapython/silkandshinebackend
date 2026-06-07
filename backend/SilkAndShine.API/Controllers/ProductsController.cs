using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.Models;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(
        AppDbContext context
    )
    {
        _context = context;
    }

    // GET api/Products
    [HttpGet]
    [HttpGet("{id}")]
    public IActionResult GetProduct(
        int id
    )
    {
        var product =
            _context.Products
            .FirstOrDefault(
                x => x.Id == id
            );

        if(product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST api/Products
    [HttpPost]
    public IActionResult AddProduct(
        Product product
    )
    {
        _context.Products.Add(product);

        _context.SaveChanges();

        return Ok(product);
    }

[HttpPut("{id}")]
public IActionResult UpdateProduct(
    int id,
    Product product
)
{
    var existing =
        _context.Products
        .FirstOrDefault(
            x => x.Id == id
        );

    if(existing == null)
    {
        return NotFound();
    }

    existing.Name =
        product.Name;

    existing.Description =
        product.Description;

    existing.Price =
        product.Price;

    existing.ImageUrl =
        product.ImageUrl;

    existing.Category =
        product.Category;

    existing.Stock =
        product.Stock;

    _context.SaveChanges();

    return Ok();
}

[HttpDelete("{id}")]
public IActionResult DeleteProduct(
    int id
)
{
    var product =
        _context.Products
        .FirstOrDefault(
            x => x.Id == id
        );

    if(product == null)
    {
        return NotFound();
    }

    _context.Products.Remove(
        product
    );

    _context.SaveChanges();

    return Ok();
}

[HttpGet("related/{category}")]
public IActionResult RelatedProducts(
    string category
)
{
    return Ok(
        _context.Products
        .Where(
            x => x.Category == category
        )
        .Take(4)
        .ToList()
    );
}
}