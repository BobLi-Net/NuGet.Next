using NuGet.Next.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NuGet.Next.Gcp;
using NuGet.Next.Options;

namespace NuGet.Next
{
    public static class GoogleCloudApplicationExtensions
    {
        public static BaGetApplication AddGoogleCloudStorage(this BaGetApplication app)
        {
            app.Services.AddBaGetOptions<GoogleCloudStorageOptions>(nameof(NuGetNextOptions.Storage));
            app.Services.AddTransient<GoogleCloudStorageService>();

            app.Services.TryAddTransient<IStorageService>(provider => provider.GetRequiredService<GoogleCloudStorageService>());

            app.Services.AddProvider<IStorageService>((provider, config) =>
            {
                if (!config.HasStorageType("GoogleCloud")) return null;

                return provider.GetRequiredService<GoogleCloudStorageService>();
            });

            return app;
        }

        public static BaGetApplication AddGoogleCloudStorage(
            this BaGetApplication app,
            Action<GoogleCloudStorageOptions> configure)
        {
            app.AddGoogleCloudStorage();
            app.Services.Configure(configure);
            return app;
        }
    }
}
