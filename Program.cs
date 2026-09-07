using CareCommerece.feature.appointments;
using CareCommerece.feature.clinics;
using CareCommerece.feature.supplies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<ClinicStore>();

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

app.MapClinics();
app.MapSupplies();
app.MapAppointments();
app.Run();
