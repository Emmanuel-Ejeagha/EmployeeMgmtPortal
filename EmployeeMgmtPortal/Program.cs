using System.Reflection;
using EmployeeMgmtPortal.Data;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpLogging(options =>
    options.LoggingFields = HttpLoggingFields.RequestProperties);
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Information);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management Portal API",
        Version = "v1",
        Description = "API for managing employee records",
        Contact = new OpenApiContact
        {
            Name = "API Support",
            Email = "support@example.com"
        }
    });

    // Try to load XML documentation
    try
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        
        if (File.Exists(xmlPath))
        {
            config.IncludeXmlComments(xmlPath);
        }
        else
        {
            Console.WriteLine($"⚠️ XML documentation file not found: {xmlPath}");
            Console.WriteLine("ℹ️ To generate XML docs, add to .csproj:");
            Console.WriteLine("  <GenerateDocumentationFile>true</GenerateDocumentationFile>");
            Console.WriteLine("  <NoWarn>$(NoWarn);1591</NoWarn>");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Error loading XML documentation: {ex.Message}");
    }

    config.EnableAnnotations();
});

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));

builder.Services.AddCors(opts =>
    opts.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();