namespace CareCommerece.feature.supplies;

public record Supply(Guid Id, string Name, string Unit, decimal Price);

public static class SupplyEndpoints
{
    public static void MapSupplies(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/supplies").WithTags("Supplies");
        
        group.MapGet("/", () => Results.Ok(new[]
        {
            new Supply(Guid.NewGuid(), "Surgical Gloves (M)", "box of 100", 850m),
            new Supply(Guid.NewGuid(), "Disposable Syringe 5ml", "box of 50", 420m)
        })).WithName("GetAllSupplies");
    }
}
