using AccessControl.Automappers;
using AccessControl.DTOs;
using Repository.Models;
using Repository.Repository;
using AccessControl.Services;
using AccessControl.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Services
    //AccessVisitor
builder.Services.AddScoped<ICreateService<AccessVisitorDto, AccessVisitorInsertDto>, AccessVisitorService>();
builder.Services.AddScoped<IReadService<AccessVisitorDto>, AccessVisitorService>();
builder.Services.AddScoped<IUpdateService<AccessVisitorDto, AccessVisitorUpdateDto>, AccessVisitorService>();
builder.Services.AddScoped<IDeleteService<AccessVisitorDto>, AccessVisitorService>();
    //Car
builder.Services.AddScoped<ICreateService<CarDto, CarInsertDto>, CarService>();
builder.Services.AddScoped<IReadService<CarDto>, CarService>();
builder.Services.AddScoped<IDeleteService<CarDto>, CarService>();
    //Visitor
builder.Services.AddScoped<ICreateService<VisitorDto, VisitorInsertDto>, VisitorService>();
builder.Services.AddScoped<IReadService<VisitorDto>, VisitorService>();
builder.Services.AddScoped<IDeleteService<VisitorDto>, VisitorService>();
    //UserAc
builder.Services.AddScoped<ISessionService<UserAcDto>, UserAcService>();
    //Device
builder.Services.AddScoped<IReadService<DeviceDto>, DeviceService>();

//Repositorys
builder.Services.AddScoped<IRepository<AccessVisitor>, AccessVisitorRepository>();
builder.Services.AddScoped<IRepository<Car>, CarRepository>();
builder.Services.AddScoped<IRepository<Visitor>, VisitorRepository>();
builder.Services.AddScoped<IRepository<UserAc>, UserAcRepository>();
builder.Services.AddScoped<IRepository<Device>, DeviceRepository>();
builder.Services.AddScoped<IRepository<Zone>, ZoneRepository>();
builder.Services.AddScoped<IRepository<House>, HouseRepository>();

//BD
IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("repositorySettings.json", optional: true, reloadOnChange: true)
        .Build();
builder.Services.AddDbContext<ControlAccessContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccessControlConnection"));
});

//Validators
builder.Services.AddScoped<IValidator<CarInsertDto>, CarInsertValidator>();
builder.Services.AddScoped<IValidator<VisitorInsertDto>, VisitorInsertValidator>();

builder.Services.AddScoped<IValidator<AccessVisitorInsertDto>, AccessVisitorInsertValidator>();
builder.Services.AddScoped<IValidator<AccessVisitorUpdateDto>, AccessVisitorUpdateValidator>();

//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Authentication
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserZonePolicy", policy => policy.RequireRole("UserZone"));
    options.AddPolicy("UserResidentialPolicy", policy => policy.RequireRole("UserResidential"));
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signingKey,
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
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
