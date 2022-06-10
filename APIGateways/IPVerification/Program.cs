using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using IPVerification.Services;
using IPVerification.Services.GeoIPService;
using IPVerification.Services.RDAPService;
using IPVerification.Services.ReverseDNS;
using IPVerification.Services.PingService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGeoIPService, GeoIPService>();
builder.Services.AddScoped<IRDAPService, RDAPService>();
builder.Services.AddScoped<IReverseDnsService, ReverseDnsService>();
builder.Services.AddScoped<IIPVerificationService, IPVerificationService>();
builder.Services.AddScoped<PingService>();

builder.Services.AddHttpClient<IGeoIPService, GeoIPService>();
builder.Services.AddHttpClient<IRDAPService, RDAPService>();
builder.Services.AddHttpClient<IReverseDnsService, ReverseDnsService>();

builder.Services.AddHttpClient("HandlerLifetime")
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));
builder.Services.AddHttpClient("ConfiguredHttpMessageHandler")
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseDefaultCredentials = true
        });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "IP Specifications API",
        Description = "An ASP.NET Core Web API for identifying IP details"
    });
    options.ExampleFilters();

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename)); 
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();


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
