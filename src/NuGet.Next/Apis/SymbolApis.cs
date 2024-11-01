using Gnarly.Data;
using NuGet.Next.Core;
using NuGet.Next.Extensions;
using NuGet.Next.Options;

namespace NuGet.Next.Service;

public class SymbolApis(
    IAuthenticationService authentication,
    ISymbolIndexingService indexer,
    ISymbolStorageService storage,
    NuGetNextOptions options,
    ILogger<SymbolApis> logger) : IScopeDependency
{
        // See: https://docs.microsoft.com/en-us/nuget/api/package-publish-resource#push-a-package
        public async Task Upload(HttpContext context)
        {
            if (options.IsReadOnlyMode || !await authentication.AuthenticateAsync(context))
            {
                context.Response.StatusCode = 401;
                return;
            }

            try
            {
                using (var uploadStream = await context.GetUploadStreamOrNullAsync(context.RequestAborted))
                {
                    if (uploadStream == null)
                    {
                        context.Response.StatusCode = 400;
                        return;
                    }

                    var result = await indexer.IndexAsync(uploadStream, context.RequestAborted);

                    switch (result)
                    {
                        case SymbolIndexingResult.InvalidSymbolPackage:
                            context.Response.StatusCode = 400;
                            break;

                        case SymbolIndexingResult.PackageNotFound:
                            context.Response.StatusCode = 404;
                            break;

                        case SymbolIndexingResult.Success:
                            context.Response.StatusCode = 201;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Exception thrown during symbol upload");

                context.Response.StatusCode = 500;
            }
        }

        public async Task Get(HttpContext context,string file, string key)
        {
            var pdbStream = await storage.GetPortablePdbContentStreamOrNullAsync(file, key);
            if (pdbStream == null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            context.Response.ContentType = "application/octet-stream";
            await pdbStream.CopyToAsync(context.Response.Body);
        }
}