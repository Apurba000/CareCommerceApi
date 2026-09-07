namespace CareCommerece.feature.appointments;

public record Appointment(Guid Id, Guid ClinicId, string PatientName, DateOnly Date);

public static class AppointmentEndpoints
{
    public static void MapAppointments(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/appointments").WithTags("Appointments");
        group.MapGet("/", () => Results.Ok(new[]
        {
            new Appointment(Guid.NewGuid(), Guid.NewGuid(), "Rahim Uddin", new DateOnly(2026, 9, 15))
        })).WithName("GetAllAppointments");
    }
}
