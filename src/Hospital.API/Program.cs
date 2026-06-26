using FluentValidation;
using Hospital.Application;
using Hospital.Application.Interfaces;
using Hospital.Application.UnitOfWork;
using Hospital.Infrastructure.BackgroundServices;
using Hospital.Infrastructure.Persistence;
using Hospital.Infrastructure.Persistence.Repositories;
using Hospital.Infrastructure.UnitOfWork;
using Hospital.SharedKernel.Behaviors;
using Hospital.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .CreateLogger();

builder.Host.UseSerilog();

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

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<OrderProcessingWorker>();

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