using Discite.API.Interfaces;
using Discite.API.Services;
using Discite.Data.Models;
using Discite.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

UserRepository userRepository = new();

if (userRepository[0] == null)
{
    using var hmac = new HMACSHA256();
    var user = new UserModel
    {
        Id = 0,
        Email = "admin@discite.xyz",
        UserName = "Admin",
        Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("vML74L7eUnuKRMco")),
        Salt = hmac.Key
    };
    var nuser = userRepository.Insert(user);

    userRepository.Delete(nuser.Id);
}



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

DisciteDbContext.ConnectionString = builder.Configuration.GetConnectionString("db");

builder.Services.AddCors(option =>
{
    option.AddPolicy("EnableCORS", builder =>
    {
        builder.SetIsOriginAllowed(origin => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .Build();
    });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
    c.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        var oldPaths = swagger.Paths.ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (var path in oldPaths)
        {
            swagger.Paths.Remove(path.Key);
            swagger.Paths.Add(path.Key.Replace("api", "api/api"), path.Value);
        }
    });
});

app.UseSwaggerUI();

app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();