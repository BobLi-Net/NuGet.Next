using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NuGet.Next.Options;

namespace NuGet.Next.Core;

/// <summary>
///     Validates BaGet's options, used at startup.
/// </summary>
public class ValidateStartupOptions
{
    private readonly NuGetNextOptions _root;
    private readonly ILogger<ValidateStartupOptions> _logger;

    public ValidateStartupOptions(
        NuGetNextOptions root,
        ILogger<ValidateStartupOptions> logger)
    {
        _root = root ?? throw new ArgumentNullException(nameof(root));
        _logger = logger;
    }

    public bool Validate()
    {
        try
        {
            _root.Validate();
            return true;
        }
        catch (OptionsValidationException e)
        {
            foreach (var failure in e.Failures) _logger.LogError("{OptionsFailure}", failure);

            _logger.LogError(e, "BaGet configuration is invalid.");
            return false;
        }
    }
}