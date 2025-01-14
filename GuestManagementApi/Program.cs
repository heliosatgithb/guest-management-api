using GuestManagementApi.Data;
using GuestManagementApi.Models;
using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.CQRS.Commands;
using GuestManagementApi.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure and use Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommandHandler<AddGuestCommand>, AddGuestCommandHandler>();
builder.Services.AddScoped<ICommandHandler<AddPhoneCommand>, AddPhoneCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllGuestsQuery, List<Guest>>, GetAllGuestsQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetGuestByIdQuery, Guest>, GetGuestByIdQueryHandler>();


// Register the in-memory database
builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("GuestDb"));

var app = builder.Build();

// Ensure Serilog is properly closed when the application shuts down
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();