using Microsoft.EntityFrameworkCore;

namespace CustomerXAPI.Data.Seeders
{
    public abstract class DbContextSeed
    {
        public abstract Task SeedAsync(DbContext context);
    }
}
