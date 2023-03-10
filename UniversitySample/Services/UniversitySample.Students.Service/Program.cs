using System.Collections.Immutable;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UniversitySample.Shared;
using UniversitySample.Students.Domain.Dto;
using UniversitySample.Students.Domain.Validations;
using UniversitySample.Students.Service;
using UniversitySample.Students.Service.DataAccess;
using UniversitySample.Students.Service.Mapping;
using UniversitySample.Students.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStudentDb();
builder.Services.AddAutoMapper(typeof(StudentProfile).Assembly);
builder.Services.AddScoped<IValidator<StudentDetailsDto>, StudentValidator>();
builder.Services.AddTransient<StudentService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var authority = builder.Configuration["Identity:Authority"];
var audience = builder.Configuration["Identity:Audience"];

builder.Services
    .AddTransient<IClaimsTransformation>(_ => new KeycloakRolesClaimsTransformer("roles", audience))
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = authority;
        options.Audience = audience;
        options.RequireHttpsMetadata = false; //UNSECURE!!!
        options.TokenValidationParameters.ValidateIssuer = false;
        options.IncludeErrorDetails = true;
        //options.Events.OnAuthenticationFailed += context =>
        //{
        //    Console.WriteLine($"Authentication failed: {context?.Exception?.Message}");
        //    return Task.FromResult(0);
        //};
        //options.Events.OnForbidden += async context => Console.WriteLine($"Authentication forbidden");
        options.SaveToken = true;
        options.TokenValidationParameters.RoleClaimType = "roles";
        options.TokenValidationParameters.NameClaimType = "preferred_username";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<StudentDbContext>>();
var dbContext = dbContextFactory.CreateDbContext();
dbContext.Database.Migrate();

app.Run();
