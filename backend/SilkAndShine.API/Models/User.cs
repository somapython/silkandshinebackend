namespace SilkAndShineAPI.Models;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Mobile { get; set; } = string.Empty;

    public string PinHash { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string Role { get; set; } = "Customer";

    public DateTime CreatedAt { get; set; }
}