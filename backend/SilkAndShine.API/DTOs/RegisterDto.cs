namespace SilkAndShineAPI.DTOs;

public class RegisterDto
{
    public string FullName { get; set; } = "";

    public string Mobile { get; set; } = "";

    public string Pin { get; set; } = "";

    public string? Email { get; set; }

    public string? Password { get; set; }
}