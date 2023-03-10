using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniSample.Common.WebApi.Security;
using UniSample.Courses.Domain.Dto;
using UniSample.Courses.Domain.Validations;
using UniSample.Courses.Service.DataAccess;
using UniSample.Courses.Service.Model;
using UniSample.Courses.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCourseDb();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Course));
builder.Services.AddScoped<IValidator<CourseDto>, CourseValidator>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddHealthChecks();

var authority = builder.Configuration["Identity:Authority"];
var audience = builder.Configuration["Identity:Audience"];

Console.WriteLine($"Authority: {authority} - Audience: {audience}");

builder.Services
    .AddTransient<IClaimsTransformation>(_ => new KeycloakRolesClaimsTransformation("roles", audience))
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
        //options.Events.OnAuthenticationFailed += context => Console.WriteLine($"Authentication failed: {context?.Exception?.Message}");
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
app.MapHealthChecks("/healthz");

//Setup Database
var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<CourseDbContext>>();
var dbContext = dbContextFactory.CreateDbContext();
//dbContext.Database.EnsureCreated();
dbContext.Database.Migrate();

app.Run();
