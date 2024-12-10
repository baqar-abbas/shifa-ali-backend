using Microsoft.EntityFrameworkCore;
using AF.Data;
using AF.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AF.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register ApplicationDbContext with SQL Server (or another DB)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories with dependency injection (DI)
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IDonationsRepository, DonationsRepository>();
builder.Services.AddScoped<IFundRaisingRepository, FundRaisingRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleCategoriesRepository, ArticleCategoriesRepository>();
builder.Services.AddScoped<PasswordService>();

// Retrieve the JWT Key from the configuration
string jwtKey = builder.Configuration["Jwt:Key"] ?? "FallbackSecretKeyForDev";

if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key is not configured.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Allow requests from localhost:3000
              .AllowAnyHeader()  // Allow any headers
              .AllowAnyMethod();  // Allow any HTTP method (GET, POST, etc.)
    });
});


// Add other services like authentication, Swagger, etc., if needed
// builder.Services.AddAuthentication().AddJwtBearer(...);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS
app.UseCors("AllowLocalhost");

// Use Authentication and Authorization
app.UseAuthentication();  // JWT Authentication
app.UseAuthorization();   // Authorization for protected routes

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Enable Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
