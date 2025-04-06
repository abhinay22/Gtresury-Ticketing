using MassTransit;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Ticketing.Consumer;
using Ticketing.Core;
using Ticketing_.Infrastructure;
using Ticketing_.Infrastructure.DBContext;
using TicketingService;
using Tickets.api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(setup =>
{
    setup.AddPolicy("Allow-all", conf =>
    {
        conf.AllowAnyOrigin();
        conf.AllowAnyMethod();
    });
});
builder.Services.AddScoped<ITicketingRepository, TicketingRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var rabbitMQUserName = builder.Configuration.GetSection("RabbitMQSettings:Username").Value;
var rabbitMQPassword = builder.Configuration.GetSection("RabbitMQSettings:Password").Value;
builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();
    configure.AddConsumer<EventActivatedConsumer>();
    configure.UsingRabbitMq((context, conf) =>
    {
        conf.Host("rabbitmq://localhost", h =>
        {
            h.Username(rabbitMQUserName);
            h.Password(rabbitMQPassword);
        });
        conf.ConfigureEndpoints(context);
    });

});
var connectionString = builder.Configuration.GetSection("RedisConfig:ConnectionString").Value;
var sqlConnectionString = builder.Configuration.GetSection("SqlServer:ConnectionString").Value;

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));
builder.Services.AddScoped<ITicketingRepository,TicketingRepository>();
builder.Services.AddAutoMapper(typeof(TicketMapper));
builder.Services.AddScoped<ITicketingService,TicketingService.TicketingService>();
builder.Services.AddDbContext<TicketingDBContext>(options =>
{
 
    options.UseSqlServer(sqlConnectionString);
}
);

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
