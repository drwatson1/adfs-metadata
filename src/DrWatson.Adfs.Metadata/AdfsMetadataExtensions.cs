using System;
using System.Security.Cryptography.X509Certificates;

namespace DrWatson.Adfs.Metadata
{
    public static class AdfsMetadataExtensions
    {
        public static X509Certificate2 GetSigningCertificate(this AdfsMetadata obj) => new X509Certificate2(Convert.FromBase64String(obj.SigningCertificateString));
    }
}