using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SilkAndShineAPI.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(
        IConfiguration config
    )
    {
        var account =
        new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );

        _cloudinary =
            new Cloudinary(account);
    }

    public string UploadImage(
        IFormFile file
    )
    {
        using var stream =
            file.OpenReadStream();

        var uploadParams =
        new ImageUploadParams
        {
            File =
            new FileDescription(
                file.FileName,
                stream
            )
        };

        var result =
            _cloudinary.Upload(
                uploadParams
            );

        return result.Url.ToString();
    }
}