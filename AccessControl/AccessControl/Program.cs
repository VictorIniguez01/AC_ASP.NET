using AccessControl.Automappers;
using AccessControl.DTOs;
using Repository.Models;
using Repository.Repository;
using AccessControl.Services;
using AccessControl.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Services
builder.Services.AddScoped<ICommonService<AccessResidentDto, AccessResidentInsertDto, AccessResidentUpdateDto>, AccessResidentService>();
builder.Services.AddScoped<ICommonService<AccessVisitorDto, AccessVisitorInsertDto, AccessVisitorUpdateDto>, AccessVisitorService>();
builder.Services.AddScoped<ICommonService<CarDto, CarInsertDto, CarUpdateDto>, CarService>();
builder.Services.AddScoped<ICommonService<VisitorDto, VisitorInsertDto,VisitorUpdateDto>, VisitorService>();

//Repositorys
builder.Services.AddScoped<IRepository<AccessResident>, AccessResidentRepository>();
builder.Services.AddScoped<IRepository<AccessVisitor>, AccessVisitorRepository>();
builder.Services.AddScoped<IRepository<Car>, CarRepository>();
builder.Services.AddScoped<IRepository<Visitor>, VisitorRepository>();

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
builder.Services.AddScoped<IValidator<AccessResidentInsertDto>, AccessResidentInsertValidator>();
builder.Services.AddScoped<IValidator<AccessResidentUpdateDto>, AccessResidentUpdateValidator>();
builder.Services.AddScoped<IValidator<AccessVisitorInsertDto>, AccessVisitorInsertValidator>();
builder.Services.AddScoped<IValidator<AccessVisitorUpdateDto>, AccessVisitorUpdateValidator>();
builder.Services.AddScoped<IValidator<CarInsertDto>, CarInsertValidator>();
builder.Services.AddScoped<IValidator<CarUpdateDto>, CarUpdateValidator>();
builder.Services.AddScoped<IValidator<VisitorInsertDto>, VisitorInsertValidator>();
builder.Services.AddScoped<IValidator<VisitorUpdateDto>, VisitorUpdateValidator>();


//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
