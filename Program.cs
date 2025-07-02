using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TaskManagement.Data;
using TaskManagement.Data.Services;
using TaskManagement.Services;

var builder = WebApplication.CreateBuilder(args);
//DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHostedService<TaskReassigningService>();

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<TaskItemService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

AppDbInitializer.Seed(app);

app.Run();
