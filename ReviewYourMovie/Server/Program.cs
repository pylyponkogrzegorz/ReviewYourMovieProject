global using ReviewYourMovie.Shared;
global using ReviewYourMovie.Shared.Models;
global using ReviewYourMovie.Server.Models;
using Microsoft.AspNetCore.ResponseCompression;
using ReviewYourMovie.Server.Context;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ReviewYourMovie.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserContext>(Options => Options.UseSqlServer("server=LAPTOP-ODHDV0AR;database=UsersDb;trusted_connection=true"));

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
