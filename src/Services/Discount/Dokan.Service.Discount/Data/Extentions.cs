using Microsoft.EntityFrameworkCore;

namespace Dokan.Service.Discount.Data;

public static class Extentions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbContext.Database.MigrateAsync();

        return app;
    }
}
