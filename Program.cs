using Authenticate;
using Authenticate.Context;
using Authenticate.DTOs;
using Authenticate.Interfaces;
using Authenticate.Validations;
using AuthenticateDTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IJWTProvider, JWTProvider>();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
builder.Services.AddScoped<IValidator<OrganizationDto>, OrganizationValidator>();

var defName = builder.Configuration["Db:Name"];
var defHost = builder.Configuration["Db:Host"];
var defPass = builder.Configuration["Db:Pass"];
// var defPort = builder.Configuration["Db:Port"];
// var defUser = builder.Configuration["Db:User"];
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
// var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? defPort;
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;
// var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? defUser;
// var connectionString = $"Server={dbHost}, {dbPort}; Initial Catalog={dbName}; User ID=${dbUser}; Password={dbPass}; TrustServerCertificate=true;";
var connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID=sa;Password={dbPass};Initial Catalog={dbName};";
builder.Services.AddDbContext<AuthenticateContextDb>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
