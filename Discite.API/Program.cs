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



#if DEBUG
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
#endif


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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#if DEBUG
app.UseCors("EnableCORS");
#endif

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();