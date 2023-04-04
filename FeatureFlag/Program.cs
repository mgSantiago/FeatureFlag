using FeatureFlag.Constants;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection(Settings.Key).Get<Settings>();

builder.Configuration.AddAzureAppConfiguration(options =>
            options.Connect(builder.Configuration.GetConnectionString("AppConfig"))
                .UseFeatureFlags()
                .Select("FeatureFlag:*", LabelFilter.Null)
                .ConfigureRefresh(refreshOptions =>
                {
                    refreshOptions.Register("FeatureFlag:Settings:Sentinel", refreshAll: true);
                })
            );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();
builder.Services.Configure<Settings>(builder.Configuration.GetSection(Settings.Key));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAzureAppConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAzureAppConfiguration();

app.MapControllers();

app.Run();