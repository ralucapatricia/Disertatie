using IdentityApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TokenGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/login", (LoginRequest request, TokenGenerator tokenGenerator) =>
{
    return new { access_token = tokenGenerator.GenerateToken(request.Email) };

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
