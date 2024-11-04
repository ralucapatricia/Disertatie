using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Data;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetValue<string>("Jwt:Key");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppDB;Trusted_Connection=True;"));
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddScoped<API.Authentication.Services.AuthenticationService>();
builder.Services.AddScoped<API.Authentication.Services.IAuthenticationService, API.Authentication.Services.AuthenticationService>();
builder.Services.AddScoped<API.Authentication.Repositories.IAuthenticationRepository, API.Authentication.Repositories.AuthenticationRepository>();
builder.Services.AddDbContext<API.Data.StoreContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 0, 0))));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
