using Microsoft.EntityFrameworkCore;
using SilkAndShineAPI.Data;
using SilkAndShineAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();


// Database
builder.Services.AddDbContext<AppDbContext>(
options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"
        )
    );
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReact",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

var jwtSettings =
builder.Configuration.GetSection("Jwt");

var key =
Encoding.UTF8.GetBytes(
    jwtSettings["Key"]!
);

builder.Services
.AddAuthentication(
JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
    new TokenValidationParameters
    {
        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,

        ValidIssuer =
        jwtSettings["Issuer"],

        ValidAudience =
        jwtSettings["Audience"],

        IssuerSigningKey =
        new SymmetricSecurityKey(
            key
        )
    };
});

builder.Services
.AddScoped<CloudinaryService>(); 

var app = builder.Build();

// HTTPS
app.UseHttpsRedirection();

// CORS
app.UseCors("AllowReact");

app.UseAuthentication();

// Authorization
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();