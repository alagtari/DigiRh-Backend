namespace Compte.API.Data.Extensions;

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
        await SeedComptesAsync(context);
        await SeedJornalsAsync(context);
    }

    private static async Task SeedComptesAsync(ApplicationDbContext context)
    {
        if (!await context.Comptes.AnyAsync())
        {
            await context.Comptes.AddRangeAsync(InitialData.Accounts);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedJornalsAsync(ApplicationDbContext context)
    {
        if (!await context.Jornals.AnyAsync())
        {
            await context.Jornals.AddRangeAsync(InitialData.Jornals);
            await context.SaveChangesAsync();
        }
    }
}