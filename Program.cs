using CareCommerece.feature.appointments;
using CareCommerece.feature.clinics;
using CareCommerece.feature.supplies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<ClinicStore>();

builder.Services.AddCors(o => o.AddPolicy("angular-dev", p => p
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => 
    {
        o.SwaggerEndpoint("/openapi/v1.json", "CareCommerce API v1");
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("angular-dev");
app.MapGet("/health", () => Results.Ok(new { status = "ok" }))
    .WithName("Health")
    .WithTags("Health");

app.MapClinics();
app.MapSupplies();
app.MapAppointments();
app.Run();
