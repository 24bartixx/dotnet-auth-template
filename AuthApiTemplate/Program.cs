using AuthApiTemplate.Infrastructure.Data;
using AuthApiTemplate.Infrastructure.Identity;
using AuthApiTemplate.Models;
using Microsoft.AspNetCore.Identity;
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

// add the  identity system configuration for the specified User and Role types
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 7;
})
.AddRoles<IdentityRole>()                           // register RoleManager + handle details of adding a role to AspNetRoles 
.AddEntityFrameworkStores<AppDbContext>();          // give UserManger and RoleManager database-backed store

var app = builder.Build();

// create missing Identity roles
// https://learn.microsoft.com/en-us/answers/questions/1529111/how-to-set-asp-net-core-identity-role-automaticall?utm_source=chatgpt.com
await IdentitySeeder.InitializeRoles(app.Services);

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
