using CrimeService.Data;
using CrimeService.Services;
using EventBus.Messaging;
using MassTransit;
using Newtonsoft.Json.Serialization;
using PoliceService.EventBusConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine($"---> Opening in {builder.Environment.EnvironmentName} environment");

builder.Services.AddControllers()
    .AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    s.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});

builder.Services.AddScoped<ICrimeDbContext, CrimeDbContext>();
builder.Services.AddScoped<ICrimeRepository, CrimeRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<UpdateCrimeConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        
        cfg.ReceiveEndpoint(EventBusConst.CrimeUpdateQueue, c =>
        {
            c.ConfigureConsumer<UpdateCrimeConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
