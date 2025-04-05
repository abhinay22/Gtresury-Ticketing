using AutoMapper;
using EventContract;
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
builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();
    //configure.AddConsumer<EventActivated>();
    configure.UsingRabbitMq((context, conf) =>
    {
        conf.Host("amqp://localhost:15672", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });

});

builder.Services.AddDbContext<EventDBContext>(options=>
{
    var connectionString = "Server=localhost;Database=EventDBContext;User Id=sa;Password=Arora1234!;TrustServerCertificate=True;";
    options.UseSqlServer(connectionString);
}
);
builder.Services.AddAutoMapper(typeof(EventMapper));
builder.Services.AddScoped<IEventService, EventService.EventService>();
builder.Services.AddScoped<IEventRepository,EventRepository.EventRepository>();

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
