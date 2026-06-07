using Microsoft.AspNetCore.Mvc;
using SilkAndShineAPI.Services;

namespace SilkAndShineAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly CloudinaryService
    _cloudinary;

    public UploadController(
        CloudinaryService cloudinary
    )
    {
        _cloudinary =
            cloudinary;
    }

    [HttpPost]
    public IActionResult Upload(
        IFormFile file
    )
    {
        var imageUrl =
            _cloudinary
            .UploadImage(file);

        return Ok(
            new
            {
                imageUrl
            }
        );
    }
}