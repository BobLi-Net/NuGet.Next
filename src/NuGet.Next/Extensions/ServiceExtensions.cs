using NuGet.Next;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using NuGet.Next.Core;
using NuGet.Next.Options;

namespace NuGet.Next.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddNuGetNext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<NuGetNextOptions>((_) => configuration.Get<NuGetNextOptions>()!);

        services.AddTransient<IConfigureOptions<CorsOptions>, ConfigureBaGetOptions>();
        services.AddTransient<IConfigureOptions<FormOptions>, ConfigureBaGetOptions>();
        services.AddTransient<IConfigureOptions<ForwardedHeadersOptions>, ConfigureBaGetOptions>();
        services.AddTransient<IConfigureOptions<IISServerOptions>, ConfigureBaGetOptions>();
        services.AddTransient<IValidateOptions<NuGetNextOptions>, ConfigureBaGetOptions>();

        services.AddBaGetOptions<IISServerOptions>(nameof(IISServerOptions));
        services.AddBaGetApplication(application => BaGetApplication(application));

        // You can swap between implementations of subsystems like storage and search using BaGet's configuration.
        // Each subsystem's implementation has a provider that reads the configuration to determine if it should be
        // activated. BaGet will run through all its providers until it finds one that is active.
        services.AddScoped(DependencyInjectionExtensions.GetServiceFromProviders<IContext>);
        services.AddTransient(DependencyInjectionExtensions.GetServiceFromProviders<IStorageService>);
        services.AddTransient(DependencyInjectionExtensions.GetServiceFromProviders<IPackageDatabase>);
        services.AddTransient(DependencyInjectionExtensions.GetServiceFromProviders<ISearchService>);
        services.AddTransient(DependencyInjectionExtensions.GetServiceFromProviders<ISearchIndexer>);

        services.AddCors();

        services.AddHttpContextAccessor();
        services.AddTransient<IUrlGenerator, NuGetNextUrlGenerator>();

        services.AddAutoGnarly();

        return services;
    }

    public static BaGetApplication BaGetApplication(BaGetApplication app)
    {
        app.AddSqliteDatabase();


        return app;
    }

    public static void Configure(this IApplicationBuilder app, IWebHostEnvironment env,IConfiguration configuration)
    {
        var options = configuration.Get<NuGetNextOptions>();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
        }

        app.UseForwardedHeaders();
        app.UsePathBase(options.PathBase);

        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(ConfigureBaGetOptions.CorsPolicy);

    }
}