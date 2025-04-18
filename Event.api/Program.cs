using AutoMapper;
using EventContract;
using Events.api;
using EventService;
using EventTicketing.Core;
using EventTicketing.Infrastructure.DBContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var rabbitMQUserName = builder.Configuration.GetSection("RabbitMQSettings:Username").Value;
var rabbitMQPassword = builder.Configuration.GetSection("RabbitMQSettings:Password").Value;
builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();
    configure.UsingRabbitMq((context, conf) =>
    {
        conf.Host("rabbitmq://localhost", h =>
        {
            h.Username(rabbitMQUserName);
            h.Password(rabbitMQPassword);
        });
    });

});
var sqlConnectionString = builder.Configuration.GetSection("SqlServer:ConnectionString").Value;
builder.Services.AddDbContext<EventDBContext>(options =>
{
    options.UseSqlServer(sqlConnectionString);
}
);
builder.Services.AddAutoMapper(typeof(EventMapper));
builder.Services.AddScoped<IEventService, EventService.EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository.EventRepository>();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();


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
