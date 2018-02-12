using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DrWatson.Adfs.Metadata
{
    public static class AdfsMetadataExtensions
    {
        public static X509Certificate2 GetSigningCertificate(this AdfsMetadata obj) => new X509Certificate2(Convert.FromBase64String(obj.SigningCertificateString));

        public static IServiceCollection AddAdfsMetadata(this IServiceCollection services, string adfsBaseUrl)
        {
            services.AddTransient<AdfsMetadataService>(provider => new AdfsMetadataLoader(adfsBaseUrl));
            return services;
        }

        public static IServiceCollection AddAdfsMetadata(this IServiceCollection services, Func<Task<string>> loader)
        {
            services.AddTransient<AdfsMetadataService>(provider => new AdfsMetadataLoader(loader));
            return services;
        }
    }
}