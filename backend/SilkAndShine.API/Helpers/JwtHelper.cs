using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace SilkAndShineAPI.Helpers;

public class JwtHelper
{
    public static string GenerateToken(
        IConfiguration config,
        int userId,
        string mobile
    )
    {
        var key =
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                config["Jwt:Key"]!
            )
        );

        var credentials =
        new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var claims =
        new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.ToString()
            ),

            new Claim(
                ClaimTypes.MobilePhone,
                mobile
            )
        };

        var token =
        new JwtSecurityToken(

            issuer:
            config["Jwt:Issuer"],

            audience:
            config["Jwt:Audience"],

            claims: claims,

            expires:
            DateTime.Now.AddDays(7),

            signingCredentials:
            credentials
        );

        return
        new JwtSecurityTokenHandler()
        .WriteToken(token);
    }
}