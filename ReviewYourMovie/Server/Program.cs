global using ReviewYourMovie.Shared;
global using ReviewYourMovie.Shared.Models;
global using ReviewYourMovie.Server.Models;
global using ReviewYourMovie.Server.Helpers;
using Microsoft.AspNetCore.ResponseCompression;
using ReviewYourMovie.Server.Context;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ReviewYourMovie.Server.Services;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<UserContext>(sp => sp.GetRequiredService<IOptions<UserContext>>().Value);

builder.Services.AddSingleton<UserService>();

builder.Services.AddDbContext<UserContext>(Options => Options.UseSqlServer("server=LAPTOP-ODHDV0AR;database=UsersDb;User ID=admin;Password=zaq1@WSXcde3;"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = "ReviewYourMovie",
                      ValidAudience = "access",
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SuperUltraExtraLongSuperSecret!"))
                  };
              });

var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

ApiHelper.InitializeClient();

app.UseSwagger();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
