using FeatureFlag.Constants;
using Microsoft.Extensions.Azure;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureWebHost(webBuilder =>
//{
//    webBuilder.ConfigureAppConfiguration(config =>
//    {
//        var settings = config.Build();
//        config.AddAzureAppConfiguration(options =>
//            options.Connect(settings["AppConfig"]).UseFeatureFlags());
//    }).UseStartup<Program>();
//});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFeatureManagement();
var settings = builder.Configuration.GetSection("TestApp:Settings").Get<Settings>();
builder.Configuration.AddAzureAppConfiguration(options =>
            options.Connect(builder.Configuration.GetConnectionString("AppConfig"))
            .UseFeatureFlags());
builder.Services.Configure<Settings>(builder.Configuration.GetSection("TestApp:Settings"));

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
