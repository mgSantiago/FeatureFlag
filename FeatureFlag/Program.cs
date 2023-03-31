using FeatureFlag.Constants;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

//var builder = Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//            webBuilder.ConfigureAppConfiguration(config =>
//            {
//                var settings = config.Build();
//                config.AddAzureAppConfiguration(options =>
//                    options.Connect(settings["AppConfig"]).UseFeatureFlags());
//            }).UseStartup<Program>());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFeatureManagement();
string connectionString = builder.Configuration.GetConnectionString("AppConfig");
var settings = builder.Configuration.GetSection("TestApp:Settings").Get<Settings>();
builder.Configuration.AddAzureAppConfiguration(connectionString);
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
