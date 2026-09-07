namespace CareCommerece.feature.clinics;

public record CreateClinicRequest(string Name, string Address, string Phone);

public static class ClinicEndpoints
{
    public static void MapClinics(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/clinics").WithTags("Clinics");
        
        group.MapGet("/", (ClinicStore store) => Results.Ok(store.All()))
            .WithName("GetAllClinics");
        
        group.MapGet("/{id:guid}", (Guid id, ClinicStore store) =>
        {
            var clinic = store.ById(id);
            return clinic is null ? Results.NotFound() : Results.Ok(clinic);
        }).WithName("GetClinicById");
        
        group.MapPost("/", (CreateClinicRequest request, ClinicStore store) =>
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.Problem(statusCode: 400, title: "Please provide a name");
            }

            if (store.NameExists(request.Name))
            {
                return Results.Problem(statusCode: 409, title: "Clinic already exists");
            }
            
            var clinic = store.Add(new Clinic(Guid.NewGuid(), request.Name, request.Address, request.Phone));
            return Results.CreatedAtRoute("GetClinicById", new { id = clinic.Id }, clinic);
        }).WithName("CreateClinic");
    }
}