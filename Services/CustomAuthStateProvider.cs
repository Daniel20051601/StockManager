using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockManager.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    // Cambiamos a IServiceProvider para resolver AuthService en GetAuthenticationStateAsync
    private readonly IServiceProvider _serviceProvider;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(IServiceProvider serviceProvider) // Inyectar IServiceProvider
    {
        _serviceProvider = serviceProvider;
        // Obtenemos AuthService aquí para registrar los callbacks.
        // Asegúrate de que AuthService ya esté registrado como Singleton o Scoped.
        var authService = _serviceProvider.GetRequiredService<AuthService>();
        authService.SetAuthenticationCallbacks(
            email => NotifyUserAuthentication(email),
            () => NotifyUserLogout()
        );
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Resolve AuthService aquí para asegurar que tenemos la misma instancia que se usó para el login.
        using (var scope = _serviceProvider.CreateScope())
        {
            var authService = scope.ServiceProvider.GetRequiredService<AuthService>();

            try
            {
                var supabaseUser = await authService.GetUserAsync();
                if (supabaseUser != null)
                {
                    // Debes obtener el UsuarioId desde tu base de datos local usando el SupabaseId
                    // Esto es crucial para el Foreign Key Constraint
                    var contexto = scope.ServiceProvider.GetRequiredService<StockManager.Dal.Contexto>();
                    var usuarioLocal = await contexto.Usuarios.FirstOrDefaultAsync(u => u.SupabaseId == supabaseUser.Id);

                    if (usuarioLocal != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, supabaseUser.Email),
                            // *** AÑADE ESTE CLAIM CON EL ID DE TU BASE DE DATOS LOCAL ***
                            new Claim("UsuarioId", usuarioLocal.UsuarioId.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, "Supabase");
                        var claimsPrincipal = new ClaimsPrincipal(identity);
                        return new AuthenticationState(claimsPrincipal);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error getting authentication state: {ex.Message}");
                return new AuthenticationState(_anonymous);
            }
        }

        return new AuthenticationState(_anonymous);
    }

    // Modifica NotifyUserAuthentication para incluir el UsuarioId
    public void NotifyUserAuthentication(string email)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var authService = scope.ServiceProvider.GetRequiredService<AuthService>();
            // Usamos el ID que se guardó en AuthService durante el login.
            // Esto asume que NotifyUserAuthentication solo se llama *después* de un login exitoso.
            var userId = authService.CurrentLoggedInUserId;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            if (userId > 0) // Solo añadir si el ID es válido
            {
                claims.Add(new Claim("UsuarioId", userId.ToString()));
            }

            var identity = new ClaimsIdentity(claims, "Supabase");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}
