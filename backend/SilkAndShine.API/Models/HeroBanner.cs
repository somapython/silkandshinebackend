namespace SilkAndShineAPI.Models;

public class HeroBanner
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? ShopNowLink { get; set; }

    public string? LookbookLink { get; set; }

    public bool IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int DisplayOrder { get; set; }
}