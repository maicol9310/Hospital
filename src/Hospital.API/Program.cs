using Hospital.Application;
using Hospital.Application.Interfaces;
using Hospital.Application.UnitOfWork;
using Hospital.Infrastructure.Persistence;
using Hospital.Infrastructure.Persistence.Repositories;
using Hospital.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Hospital.SharedKernel.Extensions;
using FluentValidation;
using MediatR;
using Hospital.SharedKernel.Behaviors;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<Hospital.Application.Mappings.OrderProfile>());

builder.Services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseGlobalExceptionMiddleware();
app.MapControllers();
app.Run();