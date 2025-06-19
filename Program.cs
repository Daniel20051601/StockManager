using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using StockManager.Components;
using StockManager.Dal;
using StockManager.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Servicios Base
builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(new Supabase.Client(
    builder.Configuration["Supabase:Url"],
    builder.Configuration["Supabase:AnonKey"]
));

// 2. Autenticación y Autorización
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();

// 3. Servicios de la Aplicación
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ComprasService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddBlazoredToast();

// 4. Componentes Blazor y configuración relacionada
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Construir la aplicación
var app = builder.Build();

// Pipeline de la aplicación
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Middleware en orden correcto
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Mapeo de componentes
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
