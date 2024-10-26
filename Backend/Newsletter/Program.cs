using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;
using Newsletter.Services;
using Newsletter;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Newsletter.Data;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // CORS-Konfiguration
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowAny", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        // JWT Konfiguration
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        if (string.IsNullOrEmpty(jwtSettings["Key"])) {
            throw new ArgumentNullException("JWT Key is missing in appsettings.json");
        }

        // Authentifizierung und Autorisierung
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = JwtRegisteredClaimNames.UniqueName,
                };
            });

        builder.Services.AddAuthorization();

        // Kestrel-Server-Konfiguration
        builder.WebHost.ConfigureKestrel(serverOptions => {
            serverOptions.ListenAnyIP(5000); // HTTP
            serverOptions.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
        });

        // Register DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
        builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
        builder.Services.AddHttpContextAccessor();

        // Services
        builder.Services.AddScoped<IArticleService, ArticleService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IMailService, MailService>();
        builder.Services.AddScoped<INewsletterService, NewsletterService>();
        builder.Services.AddScoped<IOrganizationService, OrganizationService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<JwtTokenService>();

        // Controller und Swagger
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nentindo Newsletter", Version = "v1" });
        });

        // Build the app
        var app = builder.Build();

        // Create the SQLite database on startup
        using (var scope = app.Services.CreateScope()) {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        // Use CORS
        app.UseCors("AllowAny");

        // Middleware-Reihenfolge
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Start the app
        app.Run();
    }
}
