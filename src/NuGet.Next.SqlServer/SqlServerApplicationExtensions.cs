using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Next.Core;
using NuGet.Next.Options;

namespace NuGet.Next.SqlServer;


public static class SqlServerApplicationExtensions
{
    public static BaGetApplication AddSqlServerDatabase(this BaGetApplication app)
    {
        app.Services.AddBaGetDbContextProvider<SqlServerContext>("SqlServer", (provider, options) =>
        {
            var databaseOptions = provider.GetRequiredService<NuGetNextOptions>();

            options.UseSqlServer(databaseOptions.Database.ConnectionString);
        });

        return app;
    }

    public static BaGetApplication AddSqlServerDatabase(
        this BaGetApplication app,
        Action<DatabaseOptions> configure)
    {
        app.AddSqlServerDatabase();
        app.Services.Configure(configure);
        return app;
    }
}