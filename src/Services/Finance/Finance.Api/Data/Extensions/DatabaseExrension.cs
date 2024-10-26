namespace Finance.API.Data.Extensions;

public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedAvancesAsync(context);
        await SeedChequesAsync(context);
        await SeedCreditsAsync(context);
    }

    private static async Task SeedAvancesAsync(ApplicationDbContext context)
    {
        if (!await context.Avances.AnyAsync())
        {
            await context.Avances.AddRangeAsync(InitialData.Avances);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedChequesAsync(ApplicationDbContext context)
    {
        if (!await context.Cheques.AnyAsync())
        {
            await context.Cheques.AddRangeAsync(InitialData.Cheques);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCreditsAsync(ApplicationDbContext context)
    {
        if (!await context.Credits.AnyAsync())
        {
            await context.Credits.AddRangeAsync(InitialData.Credits);
            await context.SaveChangesAsync();
        }
    }
}