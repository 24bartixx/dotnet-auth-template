var builder = WebApplication.CreateBuilder(args);

// build the OpenAPI spec from the routes / SwaggerDocument
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
