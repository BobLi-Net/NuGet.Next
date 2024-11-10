using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NuGet.Next.Core;
using NuGet.Next.Options;

namespace NuGet.Next.MySql;

public static class MySqlApplicationExtensions
{
    public static BaGetApplication AddMySqlDatabase(this BaGetApplication app)
    {
        app.Services.AddBaGetDbContextProvider<MySqlContext>("MySql", (provider, options) =>
        {
            var databaseOptions = provider.GetRequiredService<NuGetNextOptions>();

            options.UseMySql(databaseOptions.Database.ConnectionString,
                ServerVersion.AutoDetect(databaseOptions.Database.ConnectionString));
            
            options.UseLoggerFactory((new NullLoggerFactory()));
        });

        return app;
    }

    public static BaGetApplication AddMySqlDatabase(
        this BaGetApplication app,
        Action<DatabaseOptions> configure)
    {
        app.AddMySqlDatabase();
        app.Services.Configure(configure);
        return app;
    }
}