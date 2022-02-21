using Microsoft.EntityFrameworkCore;
using PoliceService.Data;
using PoliceService.Services;
using Newtonsoft.Json.Serialization;
using MassTransit;
using PoliceService.EventBusConsumer;
using EventBus.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddNewtonsoftJson(s =>
        {
            s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            s.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        });

builder.Services.AddDbContext<PoliceDbContext>(options => options.UseInMemoryDatabase("PoliceDb"));
builder.Services.AddScoped<IPoliceRepository, PoliceRepository>();

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<NewCrimeConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConst.CrimeQueue, c =>
        {
            c.ConfigureConsumer<NewCrimeConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

PoliceSeeder.Initialize();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
