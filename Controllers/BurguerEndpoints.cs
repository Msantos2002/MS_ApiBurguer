using Microsoft.EntityFrameworkCore;
using MS_ApiBurguer.Data;
using MS_ApiBurguer.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace MS_ApiBurguer.Controllers;

public static class BurguerEndpoints
{
    public static void MapBurguerEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Burguer").WithTags(nameof(Burguer));

        group.MapGet("/", async (MsWebBurguerContextContext db) =>
        {
            return await db.Burguers.ToListAsync();
        })
        .WithName("GetAllBurguers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Burguer>, NotFound>> (int burguerid, MsWebBurguerContextContext db) =>
        {
            return await db.Burguers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.BurguerId == burguerid)
                is Burguer model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBurguerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int burguerid, Burguer burguer, MsWebBurguerContextContext db) =>
        {
            var affected = await db.Burguers
                .Where(model => model.BurguerId == burguerid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.BurguerId, burguer.BurguerId)
                    .SetProperty(m => m.Name, burguer.Name)
                    .SetProperty(m => m.WithCheese, burguer.WithCheese)
                    .SetProperty(m => m.Price, burguer.Price)
                    .SetProperty(m => m.PriceCelling, burguer.PriceCelling)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBurguer")
        .WithOpenApi();

        group.MapPost("/", async (Burguer burguer, MsWebBurguerContextContext db) =>
        {
            db.Burguers.Add(burguer);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Burguer/{burguer.BurguerId}",burguer);
        })
        .WithName("CreateBurguer")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int burguerid, MsWebBurguerContextContext db) =>
        {
            var affected = await db.Burguers
                .Where(model => model.BurguerId == burguerid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBurguer")
        .WithOpenApi();
    }
}
