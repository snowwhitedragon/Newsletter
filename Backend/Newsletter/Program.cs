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
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowAny",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        // Add services to the container.
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = JwtRegisteredClaimNames.UniqueName,
                };
            });

        builder.Services.AddAuthorization();

        // Register DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nentindo Newsletter", Version = "v1" });
        });

        var app = builder.Build();

        app.UseCors("AllowAny");

        // Configure the HTTP request pipeline.
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

        app.Run();
    }
}
