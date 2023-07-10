using SampleReact.AuthServer.Data;
using SampleReact.AuthServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static OpenIddict.Abstractions.OpenIddictConstants;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          //policy.WithOrigins("https://localhost:7121", "http://localhost:5173")
                          policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                          //;
                      });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


// Add services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Filename=app.db");
    options.UseOpenIddict();
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
    })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("connect/authorize")
            .SetLogoutEndpointUris("connect/logout")
            .SetTokenEndpointUris("connect/token")
            .SetUserinfoEndpointUris("connect/userinfo");

        options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "resourceApi", Scopes.OpenId);

        options.AllowAuthorizationCodeFlow()
            .AllowRefreshTokenFlow();

        options.AddEphemeralEncryptionKey()
            .AddEphemeralSigningKey()
            .DisableAccessTokenEncryption();

        options.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableLogoutEndpointPassthrough()
            .EnableStatusCodePagesIntegration()
            .EnableTokenEndpointPassthrough();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });


builder.Services.AddHostedService<SampleReact.AuthServer.Worker>();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
