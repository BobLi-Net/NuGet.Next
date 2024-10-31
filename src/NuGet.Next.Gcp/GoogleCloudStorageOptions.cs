using System.ComponentModel.DataAnnotations;
using NuGet.Next.Options;

namespace NuGet.Next.Gcp
{
    public class GoogleCloudStorageOptions : StorageOptions
    {
        [Required]
        public string BucketName { get; set; }
    }
}
