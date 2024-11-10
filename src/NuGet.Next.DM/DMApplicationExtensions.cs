using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using NuGet.Next.Core;
using NuGet.Next.Options;

namespace NuGet.Next.DM;

public static class DMApplicationExtensions
{
    public static BaGetApplication AddDMDatabase(this BaGetApplication app)
    {
        app.Services.AddBaGetDbContextProvider<DMContext>("DM", (provider, options) =>
        {
            var databaseOptions = provider.GetRequiredService<NuGetNextOptions>();

            options.UseDm(databaseOptions.Database.ConnectionString);
            
            options.UseLoggerFactory((new NullLoggerFactory()));
        });

        return app;
    }

    public static BaGetApplication AddDMDatabase(
        this BaGetApplication app,
        Action<DatabaseOptions> configure)
    {
        app.AddDMDatabase();
        app.Services.Configure(configure);
        return app;
    }
}