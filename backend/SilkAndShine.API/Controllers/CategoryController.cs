using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.Models;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoryController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        try
        {
            var categories =
                _context.Categories
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.ImageUrl
                })
                .ToList();

            return Ok(categories);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost]
    public IActionResult AddCategory(
        Category category
    )
    {
        _context.Categories.Add(
            category
        );

        _context.SaveChanges();

        return Ok(category);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(
        int id,
        Category category
    )
    {
        var existing =
            _context.Categories
            .FirstOrDefault(
                x => x.Id == id
            );

        if(existing == null)
        {
            return NotFound();
        }

        existing.Name =
            category.Name;

        existing.ImageUrl =
            category.ImageUrl;

        _context.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(
        int id
    )
    {
        var category =
            _context.Categories
            .FirstOrDefault(
                x => x.Id == id
            );

        if(category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(
            category
        );

        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("products/{category}")]
public IActionResult GetProductsByCategory(
    string category
)
{
    var products =
        _context.Products
        .Where(
            x => x.Category == category
        )
        .ToList();

    return Ok(products);
}
}