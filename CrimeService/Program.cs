using CrimeService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine($"---> Opening in {builder.Environment.EnvironmentName} environment");

builder.Services.AddControllers();

builder.Services.AddScoped<ICrimeDbContext, CrimeDbContext>();

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
