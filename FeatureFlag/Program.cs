using FeatureFlag.Constants;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = builder.Configuration.GetSection(Settings.Key).Get<Settings>();

builder.Configuration.AddAzureAppConfiguration(options =>
            options.Connect(builder.Configuration.GetConnectionString("AppConfig"))
                .UseFeatureFlags(featureFlagOptions => {
                })
                .Select("TestApp:*", LabelFilter.Null)
                .ConfigureRefresh(refreshOptions =>
                {
                    refreshOptions.Register("TestApp:Settings:Sentinel", refreshAll: true);
                })
            );

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