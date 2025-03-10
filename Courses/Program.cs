using Courses.Data;
using Courses.Models;
using Courses.Models.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CoursesContext>(options =>
{
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Identity and Suppress the default model state validation filter
builder.Services.AddIdentity<AppUser , IdentityRole >(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Require email confirmation
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider; // Set the email confirmation token provider

    options.Password.RequireDigit = false;   // No digit required
    options.Password.RequireLowercase = false; // No lowercase required
    options.Password.RequireUppercase = false; // No uppercase required
    options.Password.RequireNonAlphanumeric = false; // No special characters required
    options.Password.RequiredLength = 1; // Minimum length set to 1
})
                .AddEntityFrameworkStores<CoursesContext>().AddDefaultTokenProviders();

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

// Add JWT Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}) 
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<Token>();

// To suppress the default model state validation filter
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddMediatR(typeof(Program).Assembly);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
