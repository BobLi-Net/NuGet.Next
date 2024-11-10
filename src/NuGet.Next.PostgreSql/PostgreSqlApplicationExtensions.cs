using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using NuGet.Next.Core;
using NuGet.Next.Options;

namespace NuGet.Next.PostgreSql;

public static class PostgreSqlApplicationExtensions
{
    public static BaGetApplication AddPostgreSqlDatabase(this BaGetApplication app)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        
        app.Services.AddBaGetDbContextProvider<PostgreSqlContext>("PostgreSql", (provider, options) =>
        {
            var databaseOptions = provider.GetRequiredService<NuGetNextOptions>();

            options.UseNpgsql(databaseOptions.Database.ConnectionString);
            options.UseLoggerFactory((new NullLoggerFactory()));
        });

        return app;
    }

    public static BaGetApplication AddPostgreSqlDatabase(
        this BaGetApplication app,
        Action<DatabaseOptions> configure)
    {
        app.AddPostgreSqlDatabase();
        app.Services.Configure(configure);
        return app;
    }
}