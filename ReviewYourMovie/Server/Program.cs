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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<UserContext>(sp => sp.GetRequiredService<IOptions<UserContext>>().Value);

builder.Services.AddSingleton<UserService>();

builder.Services.AddDbContext<UserContext>(Options => Options.UseSqlServer("server=LAPTOP-ODHDV0AR;database=UsersDb;trusted_connection=true"));

JwtBearerOptions options(JwtBearerOptions jwtBearerOptions, string audience)
{
    jwtBearerOptions.RequireHttpsMetadata = false;
    jwtBearerOptions.SaveToken = true;
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SuperUltraExtraLongSuperSecret!")),
        ValidIssuer = "ReviewYourMovie",
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true, //validate the expiration and not before values in the token
        ClockSkew = TimeSpan.FromMinutes(1) //1 minute tolerance for the expiration date
    };
    if (audience == "access")
    {
        jwtBearerOptions.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    }
    return jwtBearerOptions;
}

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

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");



app.Run();
