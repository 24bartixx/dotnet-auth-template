using AuthApiTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- API Setup ---
// Uncomment ONE of the following depending on API style:

// 1) Minimal API
// builder.Services.AddEndpointsApiExplorer(); // Required for Swagger with minimal APIs

// 2) Controller-based API
builder.Services.AddControllers(); // Adds controllers and API Explorer automatically

// -- Swagger ---
// creates SwaggerDocument
builder.Services.AddSwaggerGen();

// add AppDbContext 
// make sure that "ConnectionStrings": {"DefaultConnection": "..."} is defined in your app settings
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();           // expose JSON SwaggerDocument --> /swagger/v2/swagger.json
    app.UseSwaggerUI();         // swagger -> /swagger
}

// middleware redirecting HTTP requests to HTTPS
app.UseHttpsRedirection();

// discover attribute-routed controller actions and register them as endpoints (routing table).
app.MapControllers();

app.Run();
