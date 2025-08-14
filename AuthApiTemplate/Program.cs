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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();           // expose JSON SwaggerDocument --> /swagger/v2/swagger.json
    app.UseSwaggerUI();         // swagger -> /swagger
}

app.UseHttpsRedirection();

app.Run();
