using NuGet.Next.Core;

namespace NuGet.Next.Extensions;

public static class HttpContextExtensions
{
    public const string ApiKeyHeader = "X-NuGet-ApiKey";

    public static async Task<Stream> GetUploadStreamOrNullAsync(this HttpContext request,
        CancellationToken cancellationToken)
    {
        // Try to get the nupkg from the multipart/form-data. If that's empty,
        // fallback to the request's body.
        Stream rawUploadStream = null;
        try
        {
            if (request.Request.HasFormContentType && request.Request.Form.Files.Count > 0)
            {
                rawUploadStream = request.Request.Form.Files[0].OpenReadStream();
            }
            else
            {
                rawUploadStream = request.Request.Body;
            }

            // Convert the upload stream into a temporary file stream to
            // minimize memory usage.
            return await rawUploadStream?.AsTemporaryFileStreamAsync(cancellationToken);
        }
        finally
        {
            rawUploadStream?.Dispose();
        }
    }

    public static string GetApiKey(this HttpContext request)
    {
        return request.Request.Headers[ApiKeyHeader].ToString();
    }
}