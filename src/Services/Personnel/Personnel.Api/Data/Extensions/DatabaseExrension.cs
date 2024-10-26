namespace Personnel.API.Data.Extensions;

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
        await SeedPersonsAsync(context);
    }

    private static async Task SeedPersonsAsync(ApplicationDbContext context)
    {
        if (!await context.Persons.AnyAsync())
        {
            await context.Persons.AddRangeAsync(InitialData.Persons);
            await context.SaveChangesAsync();
        }
    }
}