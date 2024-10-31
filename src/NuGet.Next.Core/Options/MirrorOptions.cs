using System.ComponentModel.DataAnnotations;

namespace NuGet.Next.Options;

public class MirrorOptions : IValidatableObject
{
    /// <summary>
    ///     如果启用，找不到包则会从上游源下载。
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    ///     更新的上游镜像源。
    /// </summary>
    public Uri PackageSource { get; set; }

    /// <summary>
    ///     包源是否为v2包源。
    /// </summary>
    public bool Legacy { get; set; }

    /// <summary>
    ///     从包源下载超时前的时间。
    /// </summary>
    [Range(0, int.MaxValue)]
    public int PackageDownloadTimeoutSeconds { get; set; } = 600;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Enabled && PackageSource == null)
            yield return new ValidationResult(
                $"The {nameof(PackageSource)} configuration is required if mirroring is enabled",
                new[] { nameof(PackageSource) });
    }
}