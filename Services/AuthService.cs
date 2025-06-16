// Services/AuthService.cs
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using StockManager.Dal;
using StockManager.Models;
using Supabase;
using System.Threading.Tasks;

namespace StockManager.Services;

public class AuthService
{
    private readonly Client _supabaseClient;
    private Action<string>? _userAuthenticationCallback;
    private Action? _userLogoutCallback;
    private readonly Contexto _context;

    public AuthService(Client supabaseClient, Contexto context)
    {
        _supabaseClient = supabaseClient;
        _context = context;
    }

    public void SetAuthenticationCallbacks(Action<string> authCallback, Action logoutCallback)
    {
        _userAuthenticationCallback = authCallback;
        _userLogoutCallback = logoutCallback;
    }

    public async Task<Supabase.Gotrue.User?> GetUserAsync()
    {
        await _supabaseClient.InitializeAsync();
        return _supabaseClient.Auth.CurrentUser;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _supabaseClient.Auth.SignIn(email, password);
        if (response.User != null)
        {
            // Sincronizar usuario automáticamente
            await SincronizarUsuario(response.User);
            _userAuthenticationCallback?.Invoke(response.User.Email);
            return true;
        }
        return false;
    }

    private async Task SincronizarUsuario(Supabase.Gotrue.User supabaseUser)
    {
        try
        {
            // Buscar si el usuario ya existe en la base de datos local
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.SupabaseId == supabaseUser.Id);

            if (usuarioExistente == null)
            {
                // Si no existe, crear nuevo usuario con datos de Supabase
                var nuevoUsuario = new Usuario
                {
                    SupabaseId = supabaseUser.Id,
                    Email = supabaseUser.Email,
                    Nombre = supabaseUser.UserMetadata?.GetValueOrDefault("nombre")?.ToString() ?? supabaseUser.Email,
                    NombreUsuario = supabaseUser.UserMetadata?.GetValueOrDefault("nombre_usuario")?.ToString() ?? supabaseUser.Email,
                    FotoURL = supabaseUser.UserMetadata?.GetValueOrDefault("avatar_url")?.ToString() ?? string.Empty,
                    TipoUsuarioId = 1, // Tipo de usuario por defecto
                    FechaRegistro = DateTime.UtcNow,
                    Estado = true
                };

                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Actualizar datos si es necesario
                usuarioExistente.Email = supabaseUser.Email;
                // Actualizar otros campos si es necesario
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            // Log del error
            throw new Exception("Error al sincronizar usuario", ex);
        }
    }

    public async Task LogoutAsync()
    {
        await _supabaseClient.Auth.SignOut();
        _userLogoutCallback?.Invoke();
        await Task.CompletedTask;
    }
}