using Application;
using Application.Extensions;
using Application.Seed;
using Domain.Entities.Account;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme.ToLower()
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new List<string>()
                        }
            });
});

builder.Services.AddInfrastructure(builder.Configuration).AddApplication();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters()
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenKey").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
      };
  });

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", policyBuilder =>
{
    policyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200");
}));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ProductFullAccess", policy =>
        policy.RequireRole(Roles.Admin));

    options.AddPolicy("ProductEditAccess", policy =>
        policy.RequireRole(Roles.Admin, Roles.Moderator));

    options.AddPolicy("ProductReadAccess", policy =>
        policy.RequireRole(Roles.Admin, Roles.Moderator, Roles.Customer));
});

var app = builder.Build();

#region Seed Role
using var scop = app.Services.CreateScope();
var services = scop.ServiceProvider;
var context = services.GetRequiredService<ApplicationDbContext>();
var roleManager = services.GetRequiredService<RoleManager<Role>>();

await context.Database.MigrateAsync();
await RoleSeed.SeedUsers(roleManager);
#endregion

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
