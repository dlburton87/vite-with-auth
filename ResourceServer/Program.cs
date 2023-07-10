using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using SampleReact.ResourceServer;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5173","https://localhost:5173")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:7191/connect/authorize"),
                TokenUrl = new Uri("https://localhost:7191/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "resourceApi", "Resource API" }
                }
            }
        }
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2"}
            },
            new string[] {}
        }
    });
});

#region OpenIddict
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});

// Register the OpenIddict validation components.
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        options.SetIssuer("https://localhost:7191/");
        options.AddAudiences("clientApp");
        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
        //options.UseIntrospection()
        //        .SetClientId("clientApp");
        //.SetClientSecret("dataEventRecordsSecret");
        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();
        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

builder.Services.AddScoped<IAuthorizationHandler, RequireScopeHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("dataEventRecordsPolicy", policyUser =>
    {
        policyUser.Requirements.Add(new RequireScope());
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("clientApp");
        options.OAuthScopeSeparator(" ");
        options.OAuthUsePkce();
    });
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
