using Cart.API.Extensions;
using Contracts;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureVersioning();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
