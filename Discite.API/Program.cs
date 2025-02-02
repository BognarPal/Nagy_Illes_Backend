using Discite.API.Interfaces;
using Discite.API.Services;
using Discite.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

DisciteDbContext.ConnectionString = builder.Configuration.GetConnectionString("db");

//#if DEBUG
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
//#endif


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
//if (app.Environment.IsDevelopment())
{
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
}

//#if DEBUG
app.UseCors("EnableCORS");
//#endif

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
