using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Data;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HeroController : ControllerBase
{
    private readonly AppDbContext _context;

    public HeroController(
        AppDbContext context
    )
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetHero()
    {
        return Ok(
            _context.HeroBanners
            .FirstOrDefault(
                x => x.IsActive
            )
        );
    }

    [HttpPut("{id}")]
public IActionResult UpdateHero(
    int id,
    HeroBanner hero
)
{
    var existing =
        _context.HeroBanners
        .FirstOrDefault(
            x => x.Id == id
        );

    if(existing == null)
    {
        return NotFound();
    }

    existing.Title =
        hero.Title;

    existing.Subtitle =
        hero.Subtitle;

    existing.Description =
        hero.Description;

    existing.ImageUrl =
        hero.ImageUrl;

    existing.ShopNowLink =
        hero.ShopNowLink;

    existing.LookbookLink =
        hero.LookbookLink;

    _context.SaveChanges();

    return Ok(
        "Hero Updated"
    );
}
}