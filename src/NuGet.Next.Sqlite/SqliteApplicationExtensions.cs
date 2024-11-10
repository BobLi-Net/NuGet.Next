using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using NuGet.Next.Core;
using NuGet.Next.Options;
using NuGet.Next.Sqlite;

namespace NuGet.Next
{
    public static class SqliteApplicationExtensions
    {
        public static BaGetApplication AddSqliteDatabase(this BaGetApplication app)
        {
            app.Services.AddBaGetDbContextProvider<SqliteContext>("Sqlite", (provider, options) =>
            {
                var option = provider.GetRequiredService<NuGetNextOptions>();
                options.UseSqlite(option.Database.ConnectionString);

                options.UseLoggerFactory((new NullLoggerFactory()));
            });

            return app;
        }

        public static BaGetApplication AddSqliteDatabase(
            this BaGetApplication app,
            Action<DatabaseOptions> configure)
        {
            app.AddSqliteDatabase();
            app.Services.Configure(configure);
            return app;
        }
    }
}