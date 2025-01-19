using Charity_Fundraising_DBMS;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CharityFundraisingDbmsContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            if (context.Database.EnsureCreated())
            {
                logger.LogInformation("Database created successfully");
            }

            // Apply any pending migrations
            if (context.Database.GetPendingMigrations().Any())
            {
                logger.LogInformation("Applying migrations...");
                context.Database.Migrate();
            }

            // Seed initial data if needed
            if (!context.Campaigns.Any())
            {
                logger.LogInformation("Seeding initial data...");
                // Add your seeding logic here if needed
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}
