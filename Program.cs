using CursosWebApp.Models;
using CursosWebApp.Services.Implementations;
using CursosWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CursosWebAppContext>();
builder.Services.AddTransient<ICriptografia, Criptografia>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";
        options.LogoutPath = "/Usuario/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.AccessDeniedPath= "/Usuario/Login";
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Aluno", policy => policy.RequireClaim(ClaimTypes.Role, "Aluno"));
    options.AddPolicy("Tutor", policy => policy.RequireClaim(ClaimTypes.Role, "Tutor"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();
