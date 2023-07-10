using System;
using System.Threading;
using System.Threading.Tasks;
using SampleReact.AuthServer.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SampleReact.AuthServer;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        var client = await manager.FindByClientIdAsync("clientApp");

        if (client is not null)
        {
            await manager.DeleteAsync(client);
        }

        if (true)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "clientApp",
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "Sample react client application",
                //Type = ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:5173/authentication/logout-callback"),
                    new Uri("https://localhost:5173/authentication/logout-callback")
                },
                RedirectUris =
                {
                    new Uri("https://localhost:7191/authentication/login-callback"),
                    new Uri("https://localhost:7121/swagger/oauth2-redirect.html"),
                    new Uri("https://oauth.pstmn.io/v1/callback"),
                    new Uri("http://localhost:3000/api/auth/callback/AuthServer"),
                    new Uri("http://localhost:5173/authentication/login-callback"),
                    new Uri("https://localhost:5173/authentication/login-callback")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    "scp:resourceApi"
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            });
        }

    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}